using AutoMapper;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.Core.DTO.Validators;
using InterSMeet.Core.Security;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using ObjectDesign;
using System.Security.Claims;

namespace InterSMeet.BLL.Implementations
{
    public class UserBL : IUserBL
    {
        internal ICompanyRepository CompanyRepository;
        internal IUserRepository UserRepository;
        internal IPasswordGenerator PasswordGenerator;
        internal IJwtGenerator JwtGenerator;
        internal IConfiguration Configuration;
        internal IMapper Mapper;
        internal IStudentBL StudentBL;
        internal IStudentRepository StudentRepository;

        public UserBL(
            IUserRepository userRepository, IStudentRepository studentRepository, ICompanyRepository companyRepository, IStudentBL studentBL, IMapper mapper,
            IPasswordGenerator passwordGenerator, IJwtGenerator jwtGenerator, IConfiguration configuration)
        {
            PasswordGenerator = passwordGenerator;
            JwtGenerator = jwtGenerator;
            Mapper = mapper;
            UserRepository = userRepository;
            StudentRepository = studentRepository;
            CompanyRepository = companyRepository;
            StudentBL = studentBL;
            Configuration = configuration;
        }

        /// <summary>
        /// Method used by Students and Companies for authentication.
        /// </summary>
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

            Claim? roleClaim = null;
            if (userDto.RoleId is not null)
            {
                var role = UserRepository.FindRoleById((int)userDto.RoleId);
                if (role is null) throw new NullReferenceException(nameof(role));
                roleClaim = new Claim(ClaimTypes.Role, role.Name);
            }

            return new()
            {
                User = userDto,
                AccessToken = JwtGenerator.SignAccessToken(userDto, roleClaim),
                RefreshToken = JwtGenerator.SignRefreshToken(userDto)
            };
        }

        /// <summary>
        /// Method user only by Students for registration & authentication.
        /// Note that Student.Avatar isn't included in this registration.
        /// Client-Side should upload Avatar later-on when registration is complete.
        /// </summary>
        public AuthenticatedDTO StudentSignUp(StudentSignUpDTO signUpDto)
        {
            Ensure.NotNull(signUpDto, nameof(signUpDto));

            var user = Mapper.Map<UserSignUpDTO, User>(signUpDto.UserSignUpDto);

            // Validate User data
            if (UserRepository.Exists(user)) throw new BLConflictException("User already exists, provide different username or email");
            var language = UserRepository.FindLanguageById(signUpDto.UserSignUpDto.LanguageId);
            if (language is null)
                throw new BLNotFoundException("Specified language doesn't exists");
            var province = UserRepository.FindProvinceById(signUpDto.UserSignUpDto.ProvinceId);
            if (province is null)
                throw new BLNotFoundException("Specified province doesn't exists");

            // Validate Student data
            var degree = StudentRepository.FindDegreeById(signUpDto.DegreeId);
            if (degree is null)
                throw new BLNotFoundException("Specified degree doesn't exists");

            // Assign user data
            user.Password = PasswordGenerator.Hash(user.Password);
            user.Language = language;
            user.Province = province;
            // Create user
            user = UserRepository.Create(user); // Re-assign user with created user

            // Create student
            var student = StudentSignUpDTO.ToStudent(signUpDto, degree, user);
            StudentRepository.Create(student);

            var userDto = Mapper.Map<User, UserDTO>(user);
            return new()
            {
                User = userDto,
                AccessToken = JwtGenerator.SignAccessToken(userDto),
                RefreshToken = JwtGenerator.SignRefreshToken(userDto)
            };
        }

        /// <summary>
        /// Method user only by Companies for registration & authentication.
        /// </summary>
        public AuthenticatedDTO CompanySignUp(CompanySignUpDTO signUpDto)
        {
            Ensure.NotNull(signUpDto, nameof(signUpDto));

            var user = Mapper.Map<UserSignUpDTO, User>(signUpDto.UserSignUpDto);

            // Validate User data
            if (UserRepository.Exists(user)) throw new BLConflictException("User already exists, provide different username or email");
            var language = UserRepository.FindLanguageById(signUpDto.UserSignUpDto.LanguageId);
            if (language is null)
                throw new BLNotFoundException("Specified language doesn't exists");
            var province = UserRepository.FindProvinceById(signUpDto.UserSignUpDto.ProvinceId);
            if (province is null)
                throw new BLNotFoundException("Specified province doesn't exists");
            // Assign user data
            user.Password = PasswordGenerator.Hash(user.Password);
            user.Language = language;
            user.Province = province;
            // Create user
            user = UserRepository.Create(user); // Re-assign user with created user

            // Create company
            var company = CompanySignUpDTO.ToCompany(signUpDto, user);
            CompanyRepository.Create(company);

            var userDto = Mapper.Map<User, UserDTO>(user);
            return new()
            {
                User = userDto,
                AccessToken = JwtGenerator.SignAccessToken(userDto),
                RefreshToken = JwtGenerator.SignRefreshToken(userDto)
            };
        }

        public UserDTO FindProfile(string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLNotFoundException($"User not found with Username: {username}");

            return Mapper.Map<User, UserDTO>(user);
        }

        /// <summary>
        /// Retrieve available languages.
        /// </summary>
        public IEnumerable<LanguageDTO> FindAllLanguages()
        {
            var entityList = UserRepository.FindAllLanguages();
            var dtoList = Mapper.Map<IEnumerable<Language>, IEnumerable<LanguageDTO>>(entityList);
            return dtoList;
        }

        /// <summary>
        /// Retrieve available provinces.
        /// </summary>
        public IEnumerable<ProvinceDTO> FindAllProvinces()
        {
            var entityList = UserRepository.FindAllProvinces();
            var dtoList = Mapper.Map<IEnumerable<Province>, IEnumerable<ProvinceDTO>>(entityList);
            return dtoList;
        }

        // OUT OF BUSINESS

        /// <summary>
        /// Out of business method. Maybe in the future a "admin" dashbouard could be implemented.
        /// Then this method could be useful.
        /// </summary>
        public LanguageDTO CreateLanguage(LanguageDTO languageDto)
        {
            Ensure.NotNull(languageDto, nameof(languageDto));
            if (UserRepository.FindLanguageByName(languageDto.Name) is not null)
                throw new BLConflictException("Language already exists, provide different language name");

            var language = Mapper.Map<LanguageDTO, Language>(languageDto);
            return Mapper.Map<Language, LanguageDTO>(UserRepository.CreateLanguage(language));
        }

        public void CheckEmail(string email)
        {
            Ensure.NotNull(email, nameof(email));
            if (UserRepository.FindByEmail(email) is not null)
                throw new BLConflictException("Email is taken");
        }

        public void CheckUsername(string username)
        {
            Ensure.NotNull(username, nameof(username));
            if (UserRepository.FindByUsername(username) is not null)
                throw new BLConflictException("Username is taken");
        }
    }
}
