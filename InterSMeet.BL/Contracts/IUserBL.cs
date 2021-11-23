using InterSMeet.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.BLL.Contracts
{
    public interface IUserBL
    {
        public AuthenticatedDTO SignIn(SignInDTO signInDTO);
        public AuthenticatedDTO SignUp(SignUpDTO signUpDTO);
        public IEnumerable<UserDTO> FindAll();
        public UserDTO FindById(int userId);
        public UserDTO Delete(int userId);
        public UserDTO Update(UserDTO userDTO, int userId);
    }
}
