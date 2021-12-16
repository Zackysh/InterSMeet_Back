using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;

namespace InterSMeet.DAL.Repositories.Implementations
{
    public class CompanyRepository : ICompanyRepository
    {
        public InterSMeetDbContext _context { get; set; }

        public CompanyRepository(InterSMeetDbContext context)
        {
            _context = context;
        }

        // @ CRUD

        public IEnumerable<Company> FindAll()
        {
            return _context.Companies;
        }

        public Company? FindById(int companyId)
        {
            return _context.Companies.Find(companyId);
        }

        public Company Create(Company company)
        {
            var change = _context.Companies.Add(company);
            _context.SaveChanges();
            return change.Entity;
        }

        public Company? Delete(int companyId)
        {
            var company = _context.Companies.Find(companyId);
            if (company is not null)
            {
                _context.Companies.Remove(company);
                _context.SaveChanges();
                return company;
            }
            else return null;
        }
    }
}