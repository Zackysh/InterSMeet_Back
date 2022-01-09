using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;

namespace InterSMeet.DAL.Repositories.Implementations
{
    public class OfferRepository : IOfferRepository 
    {
        public InterSMeetDbContext _context { get; set; }

        public OfferRepository(InterSMeetDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Offer> FindAll()
        {
            return _context.Offers;
        }

        public Offer? FindById(int offerId)
        {
            return _context.Offers.Find(offerId);
        }

        public IEnumerable<Offer> FindCompanyOffers(int companyId)
        {
            return _context.Offers.Where((offer) => offer.CompanyId == companyId);
        }

        public Offer Create(Offer offer, int companyId)
        {
            offer.CompanyId = companyId;
            var change = _context.Offers.Add(offer);

            _context.SaveChanges();
            return change.Entity;
        }

        public Offer? Update(Offer offer)
        {
            var existing = _context.Offers.Find(offer.OfferId);
            if (existing is not null)
            {
                existing = EntityPropertyMapper.InjectNonNull(existing, offer);
                _context.SaveChanges();
                return existing;
            }
            return null;
        }

        public Offer? Delete(int offerId)
        {
            var offer = _context.Offers.Find(offerId);
            if (offer is not null)
            {
                _context.Offers.Remove(offer);
                _context.SaveChanges();
                return offer;
            }
            else return null;

        }
    }
}