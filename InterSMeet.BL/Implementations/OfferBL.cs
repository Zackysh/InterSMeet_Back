using AutoMapper;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.Core.DTO.Offer;
using InterSMeet.Core.DTO.Validators;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;

namespace InterSMeet.BLL.Implementations
{
    public class OfferBL : IOfferBL
    {
        internal IOfferRepository OfferRepository;
        internal IUserRepository UserRepository;
        internal ICompanyRepository CompanyRepository;
        internal IStudentRepository StudentRepository;

        internal IMapper Mapper;
        public OfferBL(
            IOfferRepository offerRepository, IUserRepository userRepository, ICompanyRepository companyRepository, IStudentRepository studentRepository, IMapper mapper)
        {
            Mapper = mapper;
            OfferRepository = offerRepository;
            UserRepository = userRepository;
            CompanyRepository = companyRepository;
            StudentRepository = studentRepository;
        }

        public OfferPaginationResponseDTO Pagination(OfferPaginationOptionsDTO pagination, string username)
        {
            if (pagination.Min != null
                && pagination.Max != null
                && pagination.Max < pagination.Min
             ) throw new BLBadRequestException("Incorrect salary range, max should be greater than min");

            if (pagination.PrivateData)
            {
                var user = UserRepository.FindByUsername(username);
                if (user is null || CompanyRepository.FindById(user.UserId) is null)
                    throw new BLUnauthorizedException("Invaid access token");
                pagination.CompanyId = user.UserId;
            }
            else if (pagination.CompanyId != null)
                if (CompanyRepository.FindById((int)pagination.CompanyId) == null)
                    throw new BLNotFoundException($"Company not found with ID: {pagination.CompanyId}");

            var offers = OfferRepository.Pagination(
                        pagination.Page,
                        pagination.Size,
                        pagination.Search,
                        pagination.CompanyId,
                        pagination.DegreeId,
                        pagination.FamilyId,
                        pagination.LevelId,
                        pagination.Min,
                        pagination.Max
                    );

            return new()
            {
                Pagination = pagination,
                Offers = pagination.PrivateData
                    ? Mapper.Map<IEnumerable<Offer>, IEnumerable<PrivateOfferDTO>>(offers).Select(o =>
                    {
                        o.Applicants = FindOfferApplicants(o.OfferId);
                        return o;
                    })
                    : Mapper.Map<IEnumerable<Offer>, IEnumerable<PublicOfferDTO>>(offers).Select(o =>
                    {
                        o.ApplicantCount = OfferRepository.FindOfferApplicants(o.OfferId).Count();
                        return o;
                    })
            };
        }

        //Offers = Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferDTO>>(
        //    res

        public IEnumerable<OfferDTO> FindAll()
        {
            return Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferDTO>>(OfferRepository.FindAll());
        }

        public OfferDTO FindById(int offerId)
        {
            var offer = OfferRepository.FindById(offerId);
            if (offer is null) throw new BLNotFoundException($"Offer not found with ID: {offerId}");

            return Mapper.Map<Offer, OfferDTO>(offer);
        }

        public IEnumerable<PrivateOfferDTO> FindCompanyOffers(string username)
        {
            if (username is null) throw new();

            var company = UserRepository.FindByUsername(username);
            if (company is null) throw new BLUnauthorizedException("Invaid access token");

            return Mapper.Map<IEnumerable<Offer>, IEnumerable<PrivateOfferDTO>>(
                OfferRepository.FindCompanyOffers(company.UserId)
            ).Select(o =>
            {
                o.Applicants = FindOfferApplicants(o.OfferId);
                return o;
            });
        }

        public OfferDTO Create(CreateOfferDTO createOfferDto, string username)
        {
            if (createOfferDto is null || username is null) throw new();

            var company = UserRepository.FindByUsername(username);
            if (company is null) throw new BLUnauthorizedException("Invaid access token");
            if (createOfferDto.Degrees.Count() <= 0)
                throw new BLBadRequestException("You should provide at least one degreeId");
            foreach (int degreeId in createOfferDto.Degrees)
                if (StudentRepository.FindDegreeById(degreeId) is null)
                    throw new BLConflictException($"Degree not found with ID: {degreeId}");

            return Mapper.Map<Offer, OfferDTO>(OfferRepository.Create(Mapper.Map<CreateOfferDTO, Offer>(createOfferDto), company.UserId, createOfferDto.Degrees));
        }

        public OfferDTO Update(UpdateOfferDTO offerDto, string username, int offerId)
        {
            if (NullValidator.IsNullOrEmpty(offerDto)) throw new BLBadRequestException("You should update at least one field");

            var offerExists = OfferRepository.FindById(offerId);
            var company = UserRepository.FindByUsername(username);
            if (company is null) throw new BLUnauthorizedException("Invaid access token");
            if (offerExists is null) throw new BLNotFoundException($"Offer not found with ID: {offerId}");
            if (offerExists.CompanyId != company.UserId) throw new BLForbiddenException("You can't modify others information!");
            if (offerDto.Degrees is not null)
                foreach (var degreeId in offerDto.Degrees)
                    if (OfferRepository.FindById(degreeId) is null)
                        throw new BLConflictException($"Degree not found with ID: {degreeId}");

            var offer = Mapper.Map<UpdateOfferDTO, Offer>(offerDto);
            offer.OfferId = offerId; // assign updating offer Id so EntityFramework works

            return Mapper.Map<Offer, OfferDTO>(OfferRepository.Update(offer, offerDto.Degrees)!);
        }

        public OfferDTO Delete(int offerId, string username)
        {
            var offer = OfferRepository.FindById(offerId);
            var company = UserRepository.FindByUsername(username);
            if (company is null) throw new BLUnauthorizedException("Invaid access token");
            if (offer is null) throw new BLNotFoundException($"Offer not found with ID: {offerId}");
            if (offer.CompanyId != company.UserId) throw new BLForbiddenException("You can't modify others information!");

            OfferRepository.Delete(offerId);

            return Mapper.Map<Offer, OfferDTO>(offer);
        }

        /// ======================================================================================================================
        /// @ Private Methods
        /// ======================================================================================================================

        private IEnumerable<StudentDTO> FindOfferApplicants(int offerId)
        {
            return Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(
                OfferRepository.FindOfferApplicants(offerId)
            );
        }
    }
}
