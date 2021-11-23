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
        public ActionResult<IEnumerable<UserDTO>> FindAll()
        {
            return Ok(UserBL.FindAll());
        }

        // GET api/users/:userId
        [HttpGet("{userId}")]
        public ActionResult<UserDTO> FindById(int userId)
        {
            return null;
        }

        // POST api/users/sign-up
        [HttpPost("sign-up")]
        public ActionResult<UserDTO> SignUp(SignUpDTO value)
        {
            return null;
        }

        // POST api/users/sign-in
        [HttpPost("sign-in")]
        public ActionResult<UserDTO> SignIn(SignInDTO value)
        {
            return null;
        }

        // POST api/users
        [HttpPost]
        public ActionResult<UserDTO> Create(UserDTO value)
        {
            return null;
        }

        // PUT api/users/:userId
        [HttpPut("{userId}")]
        public ActionResult<UserDTO> Update(int userId, UserDTO useerDto)
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
