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
            // If true, pagination will retrieve student's applications.
            bool findStudentApplicationsFlag = false;
            // Validate pagination options
            if (pagination.Min != null
                && pagination.Max != null
                && pagination.Max < pagination.Min
             ) throw new BLBadRequestException("Incorrect salary range, max should be greater than min");

            // Get consumer identity
            var user = UserRepository.FindByUsername(username);
            if (user is null)
                throw new BLUnauthorizedException("Invaid access token");


            // If privateData is requested
            if (pagination.PrivateData)
            {
                // set pagination companyId in case consumer is Company
                if (CompanyRepository.FindById(user.UserId) is not null)
                    pagination.CompanyId = user.UserId;
                // toggle find applications flag in case consumer is Student
                else if (StudentRepository.FindById(user.UserId) is not null)
                    findStudentApplicationsFlag = true;
                else
                    throw new BLUnauthorizedException("Invaid access token");
            }

            else if (pagination.CompanyId != null)
                if (CompanyRepository.FindById((int)pagination.CompanyId) == null)
                    throw new BLNotFoundException($"Company not found with ID: {pagination.CompanyId}");

            var offers = OfferRepository.Pagination(
                        pagination.Page,
                        pagination.Size,
                        pagination.Search,
                        pagination.SkipExpired,
                        pagination.CompanyId,
                        findStudentApplicationsFlag ? user.UserId : null,
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
                    ? findStudentApplicationsFlag
                        ? Mapper.Map<IEnumerable<Offer>, IEnumerable<ApplicationDTO>>(offers).Select(o =>
                        {
                            o.Status = OfferRepository.FindApplicationStatus(o.OfferId, user.UserId).ToString() ?? ApplicationStatus.Pending.ToString();
                            return o;
                        })
                        : Mapper.Map<IEnumerable<Offer>, IEnumerable<PrivateOfferDTO>>(offers).Select(o =>
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
            if (createOfferDto.DeadLine == DateTime.MinValue)
                throw new BLBadRequestException("DeadLine is required");
            if (createOfferDto.DeadLine < DateTime.Now)
                throw new BLBadRequestException("You should provide a date later than current");

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

        // @ Applications

        public ApplicationDTO CreateApplication(int offerId, string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLUnauthorizedException("Invaid access token");
            if (StudentRepository.FindById(user.UserId) is null)
                throw new BLUnauthorizedException("Invaid access token");

            var offer = FindById(offerId);
            if (OfferRepository.FindApplication(offerId, user.UserId) is not null)
                throw new BLConflictException("You have already applied to this offer");
            if (offer.DeadLine < DateTime.Now)
                throw new BLConflictException("You can't apply to an expired offer");

            var application = OfferRepository.CreateApplication(offerId, user.UserId, ApplicationStatus.Pending);
            return ApplicationDTO.FromOfferDTO(offer, application.Status);
        }

        public ApplicationDTO DeleteApplication(int offerId, string username)
        {
            var user = UserRepository.FindByUsername(username);
            if (user is null) throw new BLUnauthorizedException("Invaid access token");
            if (StudentRepository.FindById(user.UserId) is null)
                throw new BLUnauthorizedException("Invaid access token");

            var offer = FindById(offerId);
            if (OfferRepository.FindApplication(offerId, user.UserId) is null)
                throw new BLConflictException("You haven't applied to this offer yet");
            var application = OfferRepository.DeleteApplication(offerId, user.UserId);
            return ApplicationDTO.FromOfferDTO(offer, application!.Status);
        }

        public ApplicantDTO UpdateApplicationStatus(int offerId, int studentId, string username, ApplicationStatusDTO status)
        {
            var user = UserRepository.FindByUsername(username);
            var student = StudentRepository.FindById(studentId);
            var offer = FindById(offerId);
            // Validate company & application
            if (user is null) throw new BLUnauthorizedException("Invaid access token");
            if (CompanyRepository.FindById(user.UserId) is null)
                throw new BLUnauthorizedException("Invaid access token");
            if (student is null) throw new BLConflictException($"Student not found with ID: ${studentId}");
            if (offer.CompanyId != user.UserId) throw new BLForbiddenException("You can't modify others information!");
            // Validate new status
            var applicationStatus = Application.GetApplicationStatus(status.Status);
            if (applicationStatus is null)
                throw new BLBadRequestException("Invalid application status, try 'Pending', 'Accepted' or 'Denied'");

            OfferRepository.UpdateApplicationStatus(offerId, studentId, (ApplicationStatus)(applicationStatus));
            return FindOfferApplicant(offerId, studentId)!;
        }
        /// ======================================================================================================================
        /// @ Private Methods
        /// ======================================================================================================================

        private IEnumerable<ApplicantDTO> FindOfferApplicants(int offerId)
        {
            return Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(
                OfferRepository.FindOfferApplicants(offerId)
            ).Select(std =>
                ApplicantDTO.FromStudentDTO(
                    std,
                    OfferRepository.FindApplicantStatus(
                        std.StudentId,
                        offerId
                    ) ?? ApplicationStatus.Pending
                )
            );
        }

        private ApplicantDTO? FindOfferApplicant(int offerId, int studentId)
        {
            return FindOfferApplicants(offerId).FirstOrDefault(o => o.StudentId == studentId);
        }
    }
}
