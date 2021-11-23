using AutoMapper;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.Core.Security;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using ObjectDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.BLL.Implementations
{
    public class UserBL : IUserBL
    {
        internal IUserRepository UserRepository;
        internal IMapper Mapper;
        internal IPasswordGenerator PasswordGenerator;
        internal IJwtGenerator JwtGenerator;
        internal IConfiguration Configuration;
        public UserBL(
            IUserRepository userRepository, IMapper mapper, IPasswordGenerator passwordGenerator,
            IJwtGenerator jwtGenerator, IConfiguration configuration)
        {
            PasswordGenerator = passwordGenerator;
            JwtGenerator = jwtGenerator;
            Mapper = mapper;
            UserRepository = userRepository;
            Configuration = configuration;
        }

        public AuthenticatedDTO SignIn(SignInDTO signInDTO)
        {
            throw new NotImplementedException();
        }

        public AuthenticatedDTO SignUp(SignUpDTO signUpDto)
        {
            Ensure.NotNull(signUpDto);

            // hash password
            signUpDto.Password = PasswordGenerator.Hash(signUpDto.Password);
            var user = Mapper.Map<SignUpDTO, User>(signUpDto);
            if (!UserRepository.Exists(user))
            {
                var userDto = Mapper.Map<User, UserDTO>(UserRepository.Create(user));
                return new()
                {
                    User = userDto,
                    AccessToken = JwtGenerator.GetJwtToken(userDto.Username, Configuration["Jwt:AccessSecret"], TimeSpan.FromMinutes(15)),
                    RefreshToken = JwtGenerator.GetJwtToken(userDto.Username, Configuration["Jwt:RefreshSecret"], TimeSpan.FromMinutes(15))
                };
            }

            throw new BLConflictException("User already exists, provide different username or password");
        }

        public IEnumerable<UserRoleDTO> FindAll()
        {
            var entityList = UserRepository.FindAll();
            var dtoList = Mapper.Map<IEnumerable<User>, IEnumerable<UserRoleDTO>>(entityList);
            return dtoList;
        }

        public UserRoleDTO FindById(int userId)
        {
            throw new NotImplementedException();
        }

        public AuthenticatedDTO Create(SignUpDTO signUpDTO)
        {
            throw new NotImplementedException();
        }

        public UserRoleDTO Update(UserRoleDTO userDTO, int userId)
        {
            throw new NotImplementedException();
        }

        public UserRoleDTO Delete(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
