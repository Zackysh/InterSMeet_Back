using InterSMeet.BLL.Contracts;
using InterSMeet.BL.Exception;
using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using InterSMeet.ApiRest.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET api/students
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<StudentDTO>> FindAll()
        {
            return Ok(StudentBL.FindAll());
        }

        // GET api/students/profile
        [HttpGet("profile")]
        [Authorize]
        public ActionResult<StudentDTO> FindProfile()
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(StudentBL.FindProfile(username));
        }

        // GET api/students/degrees
        [HttpGet("degrees")]
        public ActionResult<List<DegreeDTO>> FindAllDegrees()
        {
            return Ok(StudentBL.FindAllDegrees());
        }

        // DELETE api/students/:studentId
        [HttpDelete("{studentId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<StudentDTO> Delete(int studentId)
        {
            return Ok(StudentBL.Delete(studentId));
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
