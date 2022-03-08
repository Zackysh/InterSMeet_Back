using AutoMapper;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.Core.DTO.Validators;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;
using Stripe;

namespace InterSMeet.BLL.Implementations
{
    public class CompanyBL : ICompanyBL
    {
        internal ICompanyRepository CompanyRepository;
        internal IOfferRepository OfferRepository;
        internal IUserRepository UserRepository;
        internal IUserBL UserBL;
        internal IMapper Mapper;
        internal IAuthBL AuthBL;
        public CompanyBL(
            ICompanyRepository companyRepository, IAuthBL authBl, IOfferRepository offerRepository, IUserRepository userRepository, IUserBL userBL, IMapper mapper)
        {
            AuthBL = authBl;
            Mapper = mapper;
            CompanyRepository = companyRepository;
            UserRepository = userRepository;
            UserBL = userBL;
            OfferRepository = offerRepository;
        }

        public IEnumerable<PublicCompanyDTO> FindAll()
        {
            return Mapper.Map<IEnumerable<Company>, IEnumerable<PublicCompanyDTO>>(CompanyRepository.FindAll());
        }

        public IEnumerable<CompanyDTO> FindAllAdmin()
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

        public PublicCompanyDTO FindPublicProfile(string username)
        {
            return PublicCompanyDTO.FronCompanyDto(FindProfile(username));
        }

        public CompanyDTO Delete(int companyId)
        {
            var company = CompanyRepository.FindById(companyId);
            if (company is null) throw new BLNotFoundException($"Company not found with ID:{companyId}");

            CompanyRepository.Delete(companyId);
            UserRepository.Delete(companyId);

            return Mapper.Map<Company, CompanyDTO>(company);
        }

        public AuthenticatedDTO Update(UpdateCompanyDTO updateDto, string username)
        {
            if (updateDto is null || NullValidator.IsNullOrEmpty(updateDto)) throw new BLBadRequestException("You should update at least one field");

            var currentCompany = FindProfile(username); // check if company exists

            // If username is provided and is different from current username, check if it's available
            if (
                updateDto?.UpdateUserDto?.Username is not null
                && !username.Equals(updateDto.UpdateUserDto.Username)
                && FindProfile_(updateDto.UpdateUserDto.Username) is not null
            )
                throw new BLConflictException("Provided username isn't available");

            if (updateDto?.UpdateUserDto?.LanguageId is not null) UserBL.FindLanguageById((int)updateDto.UpdateUserDto.LanguageId);
            if (updateDto?.UpdateUserDto?.ProvinceId is not null) UserBL.FindProvinceById((int)updateDto.UpdateUserDto.ProvinceId);

            var newCompanyData = Mapper.Map<UpdateCompanyDTO, Company>(updateDto!);
            var newUserData = updateDto?.UpdateUserDto is null ? null : Mapper.Map<UpdateUserDTO, User>(updateDto.UpdateUserDto);

            if (newUserData is not null)
            {
                newUserData.UserId = currentCompany.CompanyId;
                newUserData.EmailVerified = currentCompany.EmailVerified;
                UserRepository.Update(newUserData);
            }
            newCompanyData.CompanyId = currentCompany.CompanyId;
            CompanyRepository.Update(newCompanyData);

            return AuthBL.SignAuthDTO(FindProfile(currentCompany.CompanyId));
        }

        public int CountCompanyApplicants(int companyId)
        {
            int total = 0;
            var offers = OfferRepository.FindCompanyOffers(companyId).ToList();
            foreach (var offer in offers)
            {
                total += OfferRepository.FindOfferApplicants(offer.OfferId).Count();
            }

            return total;
        }

        /// <summary>
        /// Toggle premium plan (placeholder for stripe)
        /// </summary>
        public void Premium(string username)
        {
            var company = FindProfile_(username);
            if (company is null) throw new BLUnauthorizedException("Invalid access token");

            if (company.StripeId is not null && company.StripeId.Length > 0)
            {
                company.StripeId = null;
                CompanyRepository.Update(company);
            }
            else
                AssignStripeId(company.CompanyId);

        }

        // =============================================================================================
        // @ Private Methods
        // =============================================================================================

        private CompanyDTO AssignStripeId(int companyId)
        {
            var company = CompanyRepository.FindById(companyId);

            if (company is null)
                throw new BLNotFoundException($"Company not found with ID: {companyId}");

            StripeConfiguration.ApiKey = "sk_test_51KaLr9BtBI6klZnkTdelw4K3cFpKKIl8p7m2RNueRGgYkkRkZC2jZmCRhKnyfziR5bnbxJlrszO12gyG9XTh3cg800NFbz0je7";
            if (company.StripeId is null || company.StripeId.Length <= 0)
            {
                var customerOptions = new CustomerCreateOptions
                {
                    Email = company.User.Email
                };

                var customerService = new CustomerService();
                var customer = customerService.Create(customerOptions);
                company.StripeId = customer.Id;
                CompanyRepository.Update(company);
            }

            return Mapper.Map<Company, CompanyDTO>(company);
        }

        private Company? FindProfile_(string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) return null;

            return CompanyRepository.FindById(user.UserId);
        }

        private CompanyDTO FindProfile(int companyId)
        {
            var company = CompanyRepository.FindById(companyId);
            if (company is null) throw new BLConflictException($"It appears that the user isn't linked to a comapny");

            return Mapper.Map<Company, CompanyDTO>(company);
        }
    }
}
