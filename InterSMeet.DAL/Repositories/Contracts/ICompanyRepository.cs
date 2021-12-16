using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Base;

namespace InterSMeet.DAL.Repositories.Contracts
{
    public interface ICompanyRepository : IRepository<Company>
    {
        IEnumerable<Company> FindAll();
        Company? FindById(int companyId);
        Company Create(Company company);
        Company? Delete(int companyId);
    }
}
