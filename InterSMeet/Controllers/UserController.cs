using InterSMeet.ApiRest.Utils;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterSMeet.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        internal IUserBL UserBL { get; set; }

        public UserController(IUserBL UserBL)
        {
            this.UserBL = UserBL;
        }

        // Token Management

        // POST api/users/check-access
        [HttpPost("check-access")]
        [Authorize]
        public ActionResult<StudentDTO> CheckAccessToken()
        {
            // [Authorize] tag handle access token validation,
            // returns 401 status if token isn't valid
            return Ok(); // return 200 if token is valid
        }

        // POST api/users/refresh-token
        [HttpPost("refresh")]
        [AllowAnonymous]
        public ActionResult<StudentDTO> RefreshToken()
        {
            return Ok(UserBL.RefreshToken(HttpContext.Request.Headers.First(x => x.Key == "refresh-token").Value));
        }

        // Anonymous

        // POST api/users/sign-up/student
        [HttpPost("sign-up/student")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public ActionResult<AuthenticatedDTO> StudentSignUp(StudentSignUpDTO signUpDto)
        {
            return Ok(UserBL.StudentSignUp(signUpDto));
        }

        // POST api/users/sign-up/company
        [HttpPost("sign-up/company")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public ActionResult<AuthenticatedDTO> CompanySignUp(CompanySignUpDTO signUpDto)
        {
            return Ok(UserBL.CompanySignUp(signUpDto));
        }

        // POST api/users/sign-in
        [HttpPost("sign-in")]
        [AllowAnonymous]
        public ActionResult<AuthenticatedDTO> SignIn(SignInDTO signInDto)
        {
            return Ok(UserBL.SignIn(signInDto));
        }

        // POST api/users/check/email
        [HttpPost("check/email")]
        [AllowAnonymous]
        public ActionResult<AuthenticatedDTO> CheckEmail(string email)
        {
            UserBL.CheckEmail(email);
            return Ok();
        }

        // POST api/users/check/username
        [HttpPost("check/username")]
        [AllowAnonymous]
        public ActionResult<AuthenticatedDTO> CheckUsername(string username)
        {

            UserBL.CheckUsername(username);
            return Ok();
        }

        // GET api/users/languages
        [HttpGet("languages")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<LanguageDTO>> FindAllLanguages()
        {
            return Ok(UserBL.FindAllLanguages());
        }

        // GET api/users/provinces
        [HttpGet("provinces")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<ProvinceDTO>> FindAllProvinces()
        {
            return Ok(UserBL.FindAllProvinces());
        }

        // Authorized

        // GET api/users/profile
        [HttpGet("profile")]
        [Authorize]
        public ActionResult<StudentDTO> FindProfile()
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(UserBL.FindProfile(username));
        }

        // POST api/users/languages
        [HttpPost("languages")]
        [Authorize(Roles = "Admin")]
        public ActionResult<LanguageDTO> CreateLanguage(LanguageDTO languageDto)
        {
            return Ok(UserBL.CreateLanguage(languageDto));
        }
    }
}
