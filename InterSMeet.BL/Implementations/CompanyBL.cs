using AutoMapper;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;
using ObjectDesign;

namespace InterSMeet.BLL.Implementations
{
    public class CompanyBL : ICompanyBL
    {
        internal ICompanyRepository CompanyRepository;
        internal IUserRepository UserRepository;
        internal IMapper Mapper;
        public CompanyBL(
            ICompanyRepository companyRepository, IUserRepository userRepository, IMapper mapper)
        {
            Mapper = mapper;
            CompanyRepository = companyRepository;
            UserRepository = userRepository;
        }

        public IEnumerable<CompanyDTO> FindAll()
        {
            return Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDTO>>(CompanyRepository.FindAll());
        }

        public CompanyDTO FindProfile(string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLNotFoundException($"Company not found with Username: {username}");

            var company = CompanyRepository.FindById(user.UserId);
            if (company is null) throw new BLConflictException($"It appears that the user isn't linked to a company");

            return Mapper.Map<Company, CompanyDTO>(company);
        }

        public CompanyDTO Delete(int companyId)
        {
            var company = CompanyRepository.FindById(companyId);
            if (company is null) throw new BLNotFoundException($"Company not found with ID:{companyId}");

            CompanyRepository.Delete(companyId);
            UserRepository.Delete(companyId);

            return Mapper.Map<Company, CompanyDTO>(company);
        }
    }
}
