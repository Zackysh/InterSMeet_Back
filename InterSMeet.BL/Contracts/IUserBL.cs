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
        public IEnumerable<UserRoleDTO> FindAll();
        public UserRoleDTO FindById(int userId);
        public UserRoleDTO Delete(int userId);
        public UserRoleDTO Update(UserRoleDTO userDTO, int userId);
    }
}
