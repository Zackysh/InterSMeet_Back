using AutoMapper;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.BLL.Implementations
{
    public class UserB : IUserBL
    {
        public IUserRepository UserRepository { get; set; }
        public IMapper Mapper { get; set; }

        public UserB(IUserRepository userRepository, IMapper mapper)
        {
            UserRepository = userRepository;
            Mapper = mapper;
        }

        public IEnumerable<UserDTO> FindAll()
        {

            var entityList = UserRepository.FindAll();
            var dtoList = Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(entityList);
            return dtoList;
        }

        public UserDTO Delete(int userId)
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
