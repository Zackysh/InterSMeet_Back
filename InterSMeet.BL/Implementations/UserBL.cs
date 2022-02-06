using AutoMapper;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.Core.DTO.Validators;
using InterSMeet.Core.Email;
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
        internal IStudentRepository StudentRepository;
        internal IEmailSender EmailSender;

        public UserBL(
            IUserRepository userRepository, IStudentRepository studentRepository, ICompanyRepository companyRepository, IMapper mapper,
            IPasswordService passwordGenerator, IJwtService jwtGenerator, IEmailSender emailSender, IConfiguration configuration)
        {
            PasswordGenerator = passwordGenerator;
            JwtService = jwtGenerator;
            Mapper = mapper;
            UserRepository = userRepository;
            StudentRepository = studentRepository;
            CompanyRepository = companyRepository;
            Configuration = configuration;
            EmailSender = emailSender;
        }

        // =============================================================================================
        // @ New Users
        // =============================================================================================

        // @ Registration

        public AuthenticatedDTO StudentSignUp(StudentSignUpDTO signUpDto)
        {
            var user = Mapper.Map<UserSignUpDTO, User>(signUpDto.UserSignUpDto);

            // Validate User data
            if (UserRepository.Exists(user)) throw new BLConflictException("User already exists, provide different username or email");
            FindLanguageById(user.LanguageId);
            FindProvinceById(user.ProvinceId);
            // Validate Student data
            var degree = StudentRepository.FindDegreeById(signUpDto.DegreeId);
            if (degree is null) throw new BLNotFoundException($"Degree not found with ID: {signUpDto.DegreeId}");

            // Assign user data
            user.Password = PasswordGenerator.Hash(user.Password);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.RoleId = UserRoles.Student;

            // Create user
            user = UserRepository.Create(user);
            // Create student
            var student = StudentRepository.Create(StudentSignUpDTO.ToStudent(signUpDto, user));

            return SignAuthDTO(Mapper.Map<Student, StudentDTO>(student));
        }

        public AuthenticatedDTO CompanySignUp(CompanySignUpDTO signUpDto)
        {
            var user = Mapper.Map<UserSignUpDTO, User>(signUpDto.UserSignUpDto);

            // Validate User data
            if (UserRepository.Exists(user)) throw new BLConflictException("User already exists, provide different username or email");
            FindLanguageById(user.LanguageId);
            FindProvinceById(user.ProvinceId);

            // Assign user data
            user.Password = PasswordGenerator.Hash(user.Password);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.RoleId = UserRoles.Company;

            // Create user
            user = UserRepository.Create(user); // Re-assign user with created user
            // Create company
            var company = CompanyRepository.Create(CompanySignUpDTO.ToCompany(signUpDto, user));

            return SignAuthDTO(Mapper.Map<Company, CompanyDTO>(company));
        }

        // @ Email Verification

        public void SendEmailVerification(string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLUnauthorizedException("Invalid access token");
            if (user.EmailVerified) throw new BLConflictException("Email already verified");

            // Send existing verification code
            if (user.EmailVerificationCode is not null)
            {
                EmailSender.SendEmailVerification(user.Email, user.EmailVerificationCode, username);
                return;
            }

            // Or send new verification code
            var randomCode = EmailSender.RandomCode(6);
            UserRepository.SetEmailVerificationCode(user.UserId, randomCode);
            EmailSender.SendEmailVerification(user.Email, randomCode, username);
        }

        public void EmailVerification(string verificationCode, string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLUnauthorizedException("Invalid access token");
            if (user.EmailVerified) throw new BLConflictException("Email already verified");
            if (user.EmailVerificationCode is null || !user.EmailVerificationCode.Equals(verificationCode))
                throw new BLUnauthorizedException("Wrong verification code email");

            UserRepository.SetEmailVerificationCode(user.UserId, null);
        }

        public void CheckEmail(string email)
        {
            if (UserRepository.FindByEmail(email) is not null)
                throw new BLConflictException("Email not available");
        }

        public void CheckUsername(string username)
        {
            if (UserRepository.FindByUsername(username) is not null)
                throw new BLConflictException("Username not available");
        }

        // =============================================================================================
        // @ Existing Users
        // =============================================================================================

        // @ Session

        public AuthenticatedDTO SignIn(SignInDTO signInDTO, string usertype)
        {
            // Find user
            User user = FindByCredential(signInDTO.Credential);

            if (IsStudent(user.UserId))
            {
                if (!usertype.Equals("student")) throw new BLForbiddenException("You can't sign-in as a student");
                // Check password
                if (!PasswordGenerator.CompareHash(signInDTO.Password, user.Password))
                    throw new BLUnauthorizedException("Wrong password");
                var student = StudentRepository.FindById(user.UserId);
                var std = Mapper.Map<Student, StudentDTO>(student!);
                return SignAuthDTO(std);
            }
            if (IsCompany(user.UserId))
            {
                if (!usertype.Equals("company")) throw new BLForbiddenException("You can't sign-in as a company");
                // Check password
                if (!PasswordGenerator.CompareHash(signInDTO.Password, user.Password))
                    throw new BLUnauthorizedException("Wrong password");
                var company = CompanyRepository.FindById(user.UserId);
                return SignAuthDTO(Mapper.Map<Company, CompanyDTO>(company!));
            }

            throw new BLUnauthorizedException("Your user isn't a student or a company, administrator should solve this issue");
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

        // @ Password

        public void SendRestorePassword(string credential)
        {
            if (credential is null || credential.Length <= 0)
                throw new BLBadRequestException("Credential is required");

            // Find user
            User user = FindByCredential(credential);

            if (user.ForgotPasswordCode is not null)
            {
                EmailSender.SendRestorePassword(user.Email, user.ForgotPasswordCode, credential);
                return;
            }

            var randomCode = EmailSender.RandomCode(6);
            UserRepository.SetRestorePasswordCode(user.UserId, randomCode);
            EmailSender.SendRestorePassword(user.Email, randomCode, credential);
        }

        public void CheckRestorePassword(string restorePasswordCode, string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLUnauthorizedException("Invalid access token");
            if (user.ForgotPasswordCode is null || !user.ForgotPasswordCode.Equals(restorePasswordCode))
                throw new BLUnauthorizedException("Wrong restore password code");
        }

        public void RestorePassword(string newPassword, string restorePasswordCode, string credential)
        {
            User user = FindByCredential(credential);
            if (user.ForgotPasswordCode is null || !user.ForgotPasswordCode.Equals(restorePasswordCode))
                throw new BLUnauthorizedException("Wrong restore password code");

            user.Password = PasswordGenerator.Hash(newPassword);
            UserRepository.Update(user);
            UserRepository.SetRestorePasswordCode(user.UserId, null);
        }

        public UserDTO Update(UpdateUserDTO updateDTO, string username)
        {
            if (NullValidator.IsNullOrEmpty(updateDTO)) throw new BLBadRequestException("You should update at least one field");

            FindByUsername(username); // check if student exists

            if (updateDTO?.LanguageId is not null) FindLanguageById((int)updateDTO.LanguageId);
            if (updateDTO?.ProvinceId is not null) FindProvinceById((int)updateDTO.ProvinceId);

            UserRepository.Update(Mapper.Map<UpdateUserDTO, User>(updateDTO!));
            return FindByUsername(username);
        }

        // =============================================================================================
        // @ Province
        // =============================================================================================

        public IEnumerable<ProvinceDTO> FindAllProvinces()
        {
            var entityList = UserRepository.FindAllProvinces();
            var dtoList = Mapper.Map<IEnumerable<Province>, IEnumerable<ProvinceDTO>>(entityList);
            return dtoList;
        }

        public ProvinceDTO FindProvinceById(int provinceId)
        {
            var province = UserRepository.FindProvinceById(provinceId);
            if (province is null) throw new BLNotFoundException("Specified province doesn't exists");
            return Mapper.Map<Province, ProvinceDTO>(province);
        }

        // =============================================================================================
        // @ Language
        // =============================================================================================

        public IEnumerable<LanguageDTO> FindAllLanguages()
        {
            var entityList = UserRepository.FindAllLanguages();
            var dtoList = Mapper.Map<IEnumerable<Language>, IEnumerable<LanguageDTO>>(entityList);
            return dtoList;
        }

        public LanguageDTO CreateLanguage(LanguageDTO languageDto)
        {
            if (languageDto == null) throw new();
            if (UserRepository.FindLanguageByName(languageDto.Name) is not null)
                throw new BLConflictException("Language already exists, provide different language name");

            var language = Mapper.Map<LanguageDTO, Language>(languageDto);
            return Mapper.Map<Language, LanguageDTO>(UserRepository.CreateLanguage(language));
        }

        public LanguageDTO FindLanguageById(int languageId)
        {
            var language = UserRepository.FindLanguageById(languageId);
            if (language is null) throw new BLNotFoundException("Specified language doesn't exists");
            return Mapper.Map<Language, LanguageDTO>(language);
        }

        // =============================================================================================
        // @ Private Methods
        // =============================================================================================

        // @ User

        private User FindByCredential(string credential)
        {
            // Find user
            User? user;
            if (EmailValidator.IsValidEmail(credential))
                user = UserRepository.FindByEmail(credential); // email
            else user = UserRepository.FindByUsername(credential); // username
            if (user is null) throw new BLNotFoundException($"User not found with credential: {credential}");

            return user;
        }

        private UserDTO FindByUsername(string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLNotFoundException($"User not found with Username: {username}");

            return Mapper.Map<User, UserDTO>(user);
        }

        private bool IsStudent(int userId)
        {
            return StudentRepository.FindById(userId) != null;
        }

        private bool IsCompany(int userId)
        {
            return CompanyRepository.FindById(userId) != null;
        }

        // @ Auth

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