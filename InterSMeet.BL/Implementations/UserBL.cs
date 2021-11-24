using AutoMapper;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.Core.DTO.Validators;
using InterSMeet.Core.Security;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;
using ObjectDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            Ensure.NotNull(signInDTO, nameof(signInDTO));

            User? user;
            var isEmail = EmailValidator.IsValidEmail(signInDTO.Credential);
            if (isEmail)
                user = UserRepository.FindByEmail(signInDTO.Credential);
            else user = UserRepository.FindByUsername(signInDTO.Credential);

            if (user is null)
                throw new BLNotFoundException($"User not found with credential: {signInDTO.Credential}");

            if (!PasswordGenerator.CompareHash(signInDTO.Password, user.Password))
                throw new BLUnauthorizedException("Wrong password");

            var userDto = Mapper.Map<User, UserDTO>(user); // get user
            var role = UserRepository.FindRoleById(userDto.RoleId); // and its role

            Claim? roleClaim = null;
            if (role is not null)
                roleClaim = new Claim(ClaimTypes.Role, role.Name);

            return new()
            {
                User = userDto,
                AccessToken = JwtGenerator.SignAccessToken(userDto, roleClaim),
                RefreshToken = JwtGenerator.SignRefreshToken(userDto)
            };
        }

        public AuthenticatedDTO SignUp(SignUpDTO signUpDto)
        {
            Ensure.NotNull(signUpDto, nameof(signUpDto));

            var user = Mapper.Map<SignUpDTO, User>(signUpDto);

            if (UserRepository.Exists(user)) throw new BLConflictException("User already exists, provide different username or email");
            if (UserRepository.FindLanguageById(signUpDto.LanguageId) is null)
                throw new BLNotFoundException("Specified language doesn't exists");

            // hash password
            user.Password = PasswordGenerator.Hash(signUpDto.Password);

            var userDto = Mapper.Map<User, UserDTO>(UserRepository.Create(user)); // get user
            var role = UserRepository.FindRoleById(userDto.RoleId); // and its role

            Claim? roleClaim = null;
            if (role is not null)
                roleClaim = new Claim(ClaimTypes.Role, role.Name);

            return new()
            {
                User = userDto,
                AccessToken = JwtGenerator.SignAccessToken(userDto, roleClaim),
                RefreshToken = JwtGenerator.SignRefreshToken(userDto)
            };
        }

        public IEnumerable<UserDTO> FindAll()
        {
            var entityList = UserRepository.FindAll();
            var dtoList = Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(entityList);
            return dtoList;
        }

        public UserDTO FindById(int userId)
        {
            var user = UserRepository.FindById(userId);
            if (user is null) throw new BLNotFoundException($"User not found with ID: {userId}");
            return Mapper.Map<User, UserDTO>(user);
        }

        public UserDTO Create(CreateUserDTO createDto)
        {
            Ensure.NotNull(createDto, nameof(createDto));

            var user = Mapper.Map<CreateUserDTO, User>(createDto);

            if (UserRepository.Exists(user)) throw new BLConflictException("User already exists, provide different username or email");
            // Language should exist
            if (UserRepository.FindLanguageById(createDto.LanguageId) is null)
                throw new BLNotFoundException("Specified language doesn't exists");
            // If role is provided, validate if exists
            if (createDto.RoleId is not null && UserRepository.FindRoleById((int)createDto.RoleId) is null)
                throw new BLNotFoundException("Specified role doesn't exists");

            // Hash password
            createDto.Password = PasswordGenerator.Hash(createDto.Password);

            var userDto = Mapper.Map<User, UserDTO>(UserRepository.Create(user));
            return userDto;
        }

        public UserDTO Update(UpdateUserDTO userDto, int userId)
        {
            Ensure.NotNull(userDto, nameof(userDto));
            if (UserRepository.FindById(userId) is null) throw new BLNotFoundException("User doesn't exists");

            // If role is provided, validate if exists
            if (userDto.RoleId is not null && UserRepository.FindRoleById((int)userDto.RoleId) is null)
                throw new BLNotFoundException("Specified role doesn't exists");
            // If language is provided, validate if exists
            if (userDto.LanguageId is not null && UserRepository.FindLanguageById((int)userDto.LanguageId) is null)
                throw new BLNotFoundException("Specified language doesn't exists");

            var user = Mapper.Map<UpdateUserDTO, User>(userDto);
            user.UserId = userId;

            // Ensure user has been updated correctly
            var updated = UserRepository.Update(user);
            if (updated is null) throw new Exception("Couldn't update user");

            return Mapper.Map<User, UserDTO>(updated);
        }

        public UserDTO Delete(int userId)
        {
            var user = UserRepository.FindById(userId);
            if (user is null) throw new BLNotFoundException($"User not found with ID:{userId}");

            UserRepository.Delete(userId);

            return Mapper.Map<User, UserDTO>(user);
        }

        // Foreign

        public IEnumerable<LanguageDTO> FindAllLanguages()
        {
            var entityList = UserRepository.FindAllLanguages();
            var dtoList = Mapper.Map<IEnumerable<Language>, IEnumerable<LanguageDTO>>(entityList);
            return dtoList;
        }
        public LanguageDTO CreateLanguage(LanguageDTO languageDto)
        {
            Ensure.NotNull(languageDto, nameof(languageDto));
            if (UserRepository.FindLanguageByName(languageDto.Name) is not null)
                throw new BLConflictException("Language already exists, provide different language name");

            var language = Mapper.Map<LanguageDTO, Language>(languageDto);
            return Mapper.Map<Language, LanguageDTO>(UserRepository.CreateLanguage(language));
        }
    }
}
