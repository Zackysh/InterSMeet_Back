
using InterSMeet.BLL.Contracts;
using InterSMeet.BL.Exception;
using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InterSMeet.ApiRest.Utils;

namespace InterSMeet.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        internal IStudentBL StudentBL { get; set; }

        public StudentController(IStudentBL studentBL)
        {
            StudentBL = studentBL;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<StudentDTO>> FindAll()
        {
            return Ok(StudentBL.FindAll());
        }

        [HttpGet("degrees")]
        public ActionResult<List<DegreeDTO>> FindAllDegrees()
        {
            return Ok(StudentBL.FindAllDegrees());
        }

        [HttpGet("families")]
        public ActionResult<List<FamilyDTO>> FindAllFamilies()
        {
            return Ok(StudentBL.FindAllFamilies());
        }

        [HttpGet("levels")]
        public ActionResult<List<DegreeDTO>> FindAllLevels()
        {
            return Ok(StudentBL.FindAllLevels());
        }

        [HttpPut("update-profile")]
        [Authorize(Roles = "Student")]
        public ActionResult<AuthenticatedDTO> Update(UpdateStudentDTO updateDto)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(StudentBL.Update(updateDto, username));
        }

        [HttpDelete("{studentId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<StudentDTO> Delete(int studentId)
        {
            return Ok(StudentBL.Delete(studentId));
        }

        [HttpGet("application-count")]
        public ActionResult<List<DegreeDTO>> ApplicationCount()
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(StudentBL.ApplicationCount(username));
        }

        [HttpPost("upload-avatar")]
        [Authorize]
        public IActionResult UploadAvatar()
        {
            // Get student indentity
            var username = ControllerUtils.GetUserIdentity(HttpContext);

            // Validate avatar
            IFormFile image = Request.Form.Files[0];
            if (image is null) throw new BLBadRequestException("You should provide avatar image");

            return Ok(StudentBL.UploadAvatar(ImageDTO.FronIFormFile(image), username));
        }

        [HttpGet("download-avatar")]
        [Authorize]
        public IActionResult DownloadAvatar()
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            var avatar = StudentBL.DownloadAvatarByStudent(username);
            return File(avatar.ImageData, "image/jpeg");
        }
    }
}
