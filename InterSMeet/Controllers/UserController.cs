using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterSMeet.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        internal IUserBL UserBL{ get; set; }

        public UserController(IUserBL UserBL)
        {
            this.UserBL = UserBL;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<UserRoleDTO>> FindAll()
        {
            return Ok(UserBL.FindAll());
        }

        // GET api/users/:userId
        [HttpGet("{userId}")]
        public ActionResult<UserRoleDTO> FindById(int userId)
        {
            return null;
        }

        // POST api/users/sign-up
        [HttpPost("sign-up")]
        public ActionResult<UserRoleDTO> SignUp(SignUpDTO signUpDto)
        {
            return Ok(UserBL.SignUp(signUpDto));
        }

        // POST api/users/sign-in
        [HttpPost("sign-in")]
        public ActionResult<UserRoleDTO> SignIn(SignInDTO value)
        {
            return null;
        }

        // POST api/users
        [HttpPost]
        public ActionResult<UserRoleDTO> Create(UserRoleDTO value)
        {
            return null;
        }

        // PUT api/users/:userId
        [HttpPut("{userId}")]
        public ActionResult<UserRoleDTO> Update(int userId, UserRoleDTO useerDto)
        {
            return null;
        }

        // DELETE api/users/:userId
        [HttpDelete("{userId}")]
        public void Delete(int userId)
        {
        }
    }
}
