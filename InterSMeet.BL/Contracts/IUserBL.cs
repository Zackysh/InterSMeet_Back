using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.JsonPatch;
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
        public UserDTO Create(CreateUserDTO createUserDTO);
        public UserDTO Update(UpdateUserDTO userDTO, int userId);
        public UserDTO Delete(int userId);
        public IEnumerable<LanguageDTO> FindAllLanguages();
        public LanguageDTO CreateLanguage(LanguageDTO languageDto);
    }
}
