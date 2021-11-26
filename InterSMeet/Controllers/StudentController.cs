using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ObjectDesign;
using System.Diagnostics;
using System.Security.Claims;

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

        // GET api/students/:studentId
        [HttpGet("profile")]
        [Authorize]
        public ActionResult<StudentDTO> FindProfile()
        {
            var username = UserController.GetUserIdentity(HttpContext);
            return Ok(StudentBL.FindProfile(username));
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
            var username = UserController.GetUserIdentity(HttpContext);

            // Validate avatar
            IFormFile image = Request.Form.Files[0];
            if (image is null) throw new BLBadRequestException("You should provide avatar image");

            return Ok(StudentBL.UploadAvatar(ImageDTO.FronIFormFile(image), username));
        }

        [HttpGet("download-avatar/{studentId}")]
        [AllowAnonymous]
        public IActionResult DownloadAvatar(int studentId)
        {
            var avatar = StudentBL.DownloadAvatarByStudent(studentId);
            var imgTitle = avatar.ImageTitle;
            return File(avatar.ImageData, "image/jpeg");
        }
    }
}
