using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> FindAll()
        {
            return Ok(UserBL.FindAll());
        }

        // GET api/users/:userId
        [HttpGet("{userId}")]
        public ActionResult<UserDTO> FindById(int userId)
        {
            return UserBL.FindById(userId);
        }

        // POST api/users/sign-up
        [HttpPost("sign-up")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<AuthenticatedDTO> SignUp(SignUpDTO signUpDto)
        {
            return Ok(UserBL.SignUp(signUpDto));
        }

        // POST api/users/sign-in
        [HttpPost("sign-in")]
        public ActionResult<AuthenticatedDTO> SignIn(SignInDTO signInDto)
        {
            return Ok(UserBL.SignIn(signInDto));
        }

        // POST api/users
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<UserDTO> Create(CreateUserDTO createUserDto)
        {
            return Ok(UserBL.Create(createUserDto));
        }

        // PUT api/users/:userId
        [HttpPut("{userId}")]
        public ActionResult<UserDTO> Update(int userId, UpdateUserDTO updateDto)
        {
            return Ok(UserBL.Update(updateDto, userId));
        }

        // DELETE api/users/:userId
        [HttpDelete("{userId}")]
        public ActionResult<UserDTO> Delete(int userId)
        {
            return Ok(UserBL.Delete(userId));
        }

        // Foreing
        [HttpGet("languages")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<LanguageDTO>> FindAllLanguages()
        {
            return Ok(UserBL.FindAllLanguages());
        }

        [HttpPost("languages")]
        public ActionResult<LanguageDTO> CreateLanguage(LanguageDTO languageDto)
        {
            return Ok(UserBL.CreateLanguage(languageDto));
        }
    }
}
