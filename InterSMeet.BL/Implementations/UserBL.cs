using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.BLL.Implementations
{
    public class UserB : IUserBL
    {

        public UserDTO Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> FindAll()
        {
            throw new NotImplementedException();
        }

        public UserDTO FindById(int userId)
        {
            throw new NotImplementedException();
        }

        public AuthenticatedDTO SignIn(SignInDTO signInDTO)
        {
            throw new NotImplementedException();
        }

        public AuthenticatedDTO SignUp(SignUpDTO signUpDTO)
        {
            throw new NotImplementedException();
        }

        public UserDTO Update(UserDTO userDTO, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
