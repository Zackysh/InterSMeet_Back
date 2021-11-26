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

        // GET api/users/profile
        [HttpGet("profile")]
        [Authorize]
        public ActionResult<StudentDTO> FindProfile()
        {
            var username = GetUserIdentity(HttpContext);
            return Ok(UserBL.FindProfile(username));
        }

        // POST api/users/sign-in
        [HttpPost("sign-in")]
        [AllowAnonymous]
        public ActionResult<AuthenticatedDTO> SignIn(SignInDTO signInDto)
        {
            return Ok(UserBL.SignIn(signInDto));
        }

        // Foreing

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

        // CREATE api/users/languages
        [HttpPost("languages")]
        [Authorize(Roles = "Admin")]
        public ActionResult<LanguageDTO> CreateLanguage(LanguageDTO languageDto)
        {
            return Ok(UserBL.CreateLanguage(languageDto));
        }

        public static string GetUserIdentity(HttpContext context)
        {
            if (context.User.Identity is not ClaimsIdentity claims) throw new BLUnauthorizedException("Invalid token");
            var authUsernameClaim = claims.FindFirst(ClaimTypes.Name);
            if (authUsernameClaim is null) throw new BLUnauthorizedException("Invalid token");
            var username = authUsernameClaim.Value;
            return username;
        }
    }
}
