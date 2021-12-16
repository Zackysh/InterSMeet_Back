using InterSMeet.DAL.Base;
using InterSMeet.DAL.Entities;

namespace InterSMeet.DAL.Repositories.Contracts
{
    public interface IUserRepository : IRepository<Student>
    {
        IEnumerable<User> FindAll();
        User? FindById(int userId);
        User? FindByUsername(string username);
        User? FindByEmail(string email);
        User Create(User user);
        User? Update(User user);
        User? Delete(int userId);
        bool Exists(User user);
        // Foreing
        IEnumerable<Language> FindAllLanguages();
        Language? FindLanguageById(int languageId);
        IEnumerable<Province> FindAllProvinces();
        Province? FindProvinceById(int provinceId);
        Language? FindLanguageByName(string name);
        Language CreateLanguage(Language language);
        UserRole? FindRoleById(int rooleId);
    }
}
