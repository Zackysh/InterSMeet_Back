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

        public IEnumerable<Company> FindAll()
        {
            var companies = _context.Companies
            .Join(
                _context.Users,
                s => s.CompanyId,
                u => u.UserId,
                (company, user) => new { company, user }
            );

            foreach (var comp in companies)
                comp.company.User = comp.user;

            return companies.Select(full => full.company);
        }

        public Company? FindById(int companyId)
        {
            var comp = _context.Companies.Find(companyId);
            if (comp == null) return comp;
            comp.User = _context.Users.Find(companyId)!;
            return comp;
        }

        public Company Create(Company company)
        {
            var change = _context.Companies.Add(company);
            _context.SaveChanges();
            return FindById(change.Entity.CompanyId)!;
        }

        public Company? Update(Company company)
        {
            var existing = _context.Companies.Find(company.CompanyId);
            if (existing is not null)
            {
                existing = EntityPropertyMapper.InjectNonNull(existing, company);
                _context.SaveChanges();
                return FindById(existing.CompanyId)!;
            }
            return null;
        }

        public Company? Delete(int companyId)
        {
            var company = FindById(companyId);
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