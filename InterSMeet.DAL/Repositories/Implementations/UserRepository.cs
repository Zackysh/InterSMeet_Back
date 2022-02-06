using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;

namespace InterSMeet.DAL.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public InterSMeetDbContext _context { get; set; }

        public UserRepository(InterSMeetDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> FindAll()
        {
            return _context.Users;
        }

        // @ CRUD        

        public User? FindById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User? FindByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User? FindByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User Create(User user)
        {
            var change = _context.Users.Add(user);
            _context.SaveChanges();
            return change.Entity;
        }

        public User? Update(User user)
        {
            var existing = _context.Users.Find(user.UserId);
            if (existing is not null)
            {
                existing = EntityPropertyMapper.InjectNonNull(existing, user);
                _context.SaveChanges();
                return existing;
            }
            return null;
        }

        public User? Delete(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user is not null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return user;
            }
            else return null;

        }

        public bool Exists(User user)
        {
            return _context.Users.Any(u => u.Email == user.Email || u.Username == user.Username);
        }

        // @ Codes

        public User? SetEmailVerificationCode(int userId, string? newCode)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user is not null)
            {
                user.EmailVerificationCode = newCode;
                if (newCode is null) user.EmailVerified = true;

                _context.SaveChanges();
                return user;
            }
            return null;
        }

        public User? SetRestorePasswordCode(int userId, string? newCode)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user is not null)
            {
                user.ForgotPasswordCode = newCode;
                _context.SaveChanges();
                return user;
            }
            return null;
        }

        // @ Foreing

        // ------------------------------------------------------------------------------
        // @ Role
        // ------------------------------------------------------------------------------

        public UserRole? FindRoleById(int roleId)
        {
            return _context.UserRoles.Find(roleId);
        }


        // ------------------------------------------------------------------------------
        // @ Province
        // ------------------------------------------------------------------------------

        public IEnumerable<Province> FindAllProvinces()
        {
            return _context.Provinces;
        }

        public Province? FindProvinceById(int provinceId)
        {
            return _context.Provinces.Find(provinceId);
        }


        // ------------------------------------------------------------------------------
        // @ Language
        // ------------------------------------------------------------------------------

        public IEnumerable<Language> FindAllLanguages()
        {
            return _context.Languages;
        }

        public Language? FindLanguageById(int languageId)
        {
            return _context.Languages.Find(languageId);
        }
        public Language? FindLanguageByName(string name)
        {
            return _context.Languages.FirstOrDefault(l => l.Name == name);
        }

        public Language CreateLanguage(Language language)
        {
            var change = _context.Languages.Add(language);
            _context.SaveChanges();
            return change.Entity;
        }
    }
}