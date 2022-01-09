using AutoMapper;
using InterSMeet.BL.Exception;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.Core.DTO.Validators;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;

namespace InterSMeet.BLL.Implementations
{
    public class OfferBL : IOfferBL
    {
        internal IOfferRepository OfferRepository;
        internal IUserRepository UserRepository;
        internal IMapper Mapper;
        public OfferBL(
            IOfferRepository offerRepository, IUserRepository userRepository, IMapper mapper)
        {
            Mapper = mapper;
            OfferRepository = offerRepository;
            UserRepository = userRepository;
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

        public IEnumerable<OfferDTO> FindCompanyOffers(string username)
        {
            if (username is null) throw new();

            var company = UserRepository.FindByUsername(username);
            if (company is null) throw new BLUnauthorizedException("Invaid access token");

            return Mapper.Map<IEnumerable<Offer>, IEnumerable<OfferDTO>>(OfferRepository.FindCompanyOffers(company.UserId));
        }

        public OfferDTO Create(CreateOfferDTO createOfferDto, string username)
        {
            if (createOfferDto is null || username is null) throw new();

            var company = UserRepository.FindByUsername(username);
            if (company is null) throw new BLUnauthorizedException("Invaid access token");
            // TODO validate degrees

            return Mapper.Map<Offer, OfferDTO>(OfferRepository.Create(Mapper.Map<CreateOfferDTO, Offer>(createOfferDto), company.UserId));
        }

        public OfferDTO Update(UpdateOfferDTO offerDto, string username, int offerId)
        {
            if (NullValidator.IsNullOrEmpty(offerDto)) throw new BLBadRequestException("You should update at least one field");

            var offerExists = OfferRepository.FindById(offerId);
            var company = UserRepository.FindByUsername(username);
            if (company is null) throw new BLUnauthorizedException("Invaid access token");
            if (offerExists is null) throw new BLNotFoundException($"Offer not found with ID: {offerId}");
            if (offerExists.CompanyId != company.UserId) throw new BLForbiddenException("You can't modify others information!");
            var offer = Mapper.Map<UpdateOfferDTO, Offer>(offerDto);
            offer.OfferId = offerId;

            return Mapper.Map<Offer, OfferDTO>(OfferRepository.Update(offer)!);
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
    }
}
