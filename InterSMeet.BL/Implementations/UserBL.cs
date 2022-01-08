using AutoMapper;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.Core.DTO.Validators;
using InterSMeet.Core.Security;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace InterSMeet.BLL.Implementations
{
    public class UserBL : IUserBL
    {
        internal ICompanyRepository CompanyRepository;
        internal IUserRepository UserRepository;
        internal IPasswordService PasswordGenerator;
        internal IJwtService JwtService;
        internal IConfiguration Configuration;
        internal IMapper Mapper;
        internal IStudentBL StudentBL;
        internal IStudentRepository StudentRepository;

        public UserBL(
            IUserRepository userRepository, IStudentRepository studentRepository, ICompanyRepository companyRepository, IStudentBL studentBL, IMapper mapper,
            IPasswordService passwordGenerator, IJwtService jwtGenerator, IConfiguration configuration)
        {
            PasswordGenerator = passwordGenerator;
            JwtService = jwtGenerator;
            Mapper = mapper;
            UserRepository = userRepository;
            StudentRepository = studentRepository;
            CompanyRepository = companyRepository;
            StudentBL = studentBL;
            Configuration = configuration;
        }

        public AuthenticatedDTO RefreshToken(string refreshToken)
        {
            var principal = JwtService.GetRefreshTokenPrincipal(refreshToken);
            if (principal?.Identity?.Name == null)
                throw new BLUnauthorizedException("Invalid refresh token");

            var user = UserRepository.FindByUsername(principal.Identity.Name);
            if (user == null)
                throw new BLUnauthorizedException("Invalid refresh token");

            var userDto = Mapper.Map<User, UserDTO>(user);
            return SignAuthDTO(userDto, refreshToken);
        }

        /// <summary>
        /// Method used by Students and Companies for authentication.
        /// </summary>
        public AuthenticatedDTO SignIn(SignInDTO signInDTO)
        {
            if (signInDTO == null) throw new();

            User? user;
            var isEmail = EmailValidator.IsValidEmail(signInDTO.Credential);
            if (isEmail)
                user = UserRepository.FindByEmail(signInDTO.Credential);
            else user = UserRepository.FindByUsername(signInDTO.Credential);

            if (user is null)
                throw new BLNotFoundException($"User not found with credential: {signInDTO.Credential}");

            if (!PasswordGenerator.CompareHash(signInDTO.Password, user.Password))
                throw new BLUnauthorizedException("Wrong password");

            if (IsStudent(user.UserId))
            {
                var student = StudentRepository.FindById(user.UserId);
                var std = Mapper.Map<Student, StudentDTO>(student!);
                return SignAuthDTO(std);
            }
            if (IsCompany(user.UserId))
            {
                var company = CompanyRepository.FindById(user.UserId);
                return SignAuthDTO(Mapper.Map<Company, CompanyDTO>(company!));
            }

            throw new BLUnauthorizedException("Your user isn't a student or a company, administrator should solve this issue");
        }

        /// <summary>
        /// Method user only by Students for registration & authentication.
        /// Note that Student.Avatar isn't included in this registration.
        /// Client-Side should upload Avatar later-on when registration is complete.
        /// </summary>
        public AuthenticatedDTO StudentSignUp(StudentSignUpDTO signUpDto)
        {
            if (signUpDto == null) throw new();

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
            return SignAuthDTO(userDto);
        }

        /// <summary>
        /// Method user only by Companies for registration & authentication.
        /// </summary>
        public AuthenticatedDTO CompanySignUp(CompanySignUpDTO signUpDto)
        {
            if (signUpDto == null) throw new();


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
            return SignAuthDTO(userDto);
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
            if (languageDto == null) throw new();
            if (UserRepository.FindLanguageByName(languageDto.Name) is not null)
                throw new BLConflictException("Language already exists, provide different language name");

            var language = Mapper.Map<LanguageDTO, Language>(languageDto);
            return Mapper.Map<Language, LanguageDTO>(UserRepository.CreateLanguage(language));
        }

        public void CheckEmail(string email)
        {
            if (email== null) throw new();
            if (UserRepository.FindByEmail(email) is not null)
                throw new BLConflictException("Email is taken");
        }

        public void CheckUsername(string username)
        {
            if (username == null) throw new();
            if (UserRepository.FindByUsername(username) is not null)
                throw new BLConflictException("Username is taken");
        }

        private bool IsStudent(int userId)
        {
            return StudentRepository.FindById(userId) != null;
        }

        private bool IsCompany(int userId)
        {
            return CompanyRepository.FindById(userId) != null;
        }

        private AuthenticatedDTO SignAuthDTO(UserDTO userDto, string? refreshToken = null)
        {
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
                AccessToken = JwtService.SignAccessToken(userDto.Username, roleClaim),
                RefreshToken = refreshToken ?? JwtService.SignRefreshToken(userDto.Username)
            };
        }
    }
}
