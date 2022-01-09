using AutoMapper;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;

namespace InterSMeet.BLL.Implementations
{
    public class CompanyBL : ICompanyBL
    {
        internal ICompanyRepository CompanyRepository;
        internal IUserRepository UserRepository;
        internal IUserBL UserBL;
        internal IMapper Mapper;
        public CompanyBL(
            ICompanyRepository companyRepository, IUserRepository userRepository, IUserBL userBL, IMapper mapper)
        {
            Mapper = mapper;
            CompanyRepository = companyRepository;
            UserRepository = userRepository;
            UserBL = userBL;
        }

        public IEnumerable<CompanyDTO> FindAll()
        {
            return Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDTO>>(CompanyRepository.FindAll());
        }

        public CompanyDTO Update(UpdateCompanyDTO updateDTO, string username)
        {
            if (updateDTO is null || username is null) throw new();

            FindProfile(username); // check if student exists

            if (updateDTO?.UpdateUserDto?.LanguageId is not null) UserBL.FindLanguageById((int)updateDTO.UpdateUserDto.LanguageId);
            if (updateDTO?.UpdateUserDto?.ProvinceId is not null) UserBL.FindProvinceById((int)updateDTO.UpdateUserDto.ProvinceId);

            CompanyRepository.Update(Mapper.Map<UpdateCompanyDTO, Company>(updateDTO!));
            return FindProfile(username);
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
