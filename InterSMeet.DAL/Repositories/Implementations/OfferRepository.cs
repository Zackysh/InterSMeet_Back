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

        public IEnumerable<Offer> Pagination(
            int page,
            int size,
            string? search,
            int? companyId,
            int? degreeId,
            int? familyId,
            int? levelId,
            double? minSalary,
            double? maxSalary)
        {
            IEnumerable<Offer> offers = _context.Offers;
            // @ Apply Degree Filters
            if (degreeId is not null)
            {
                offers = _context.OfferDegrees.Where(o =>
                    _context.Degrees.Find(o.DegreeId) != null
                    ? _context.Degrees.Find(o.DegreeId)!.DegreeId == degreeId
                    : false
                ).Select(od => _context.Offers.First(o => o.OfferId == od.OfferId));
            }
            else if (familyId is not null)
            {
                offers = _context.OfferDegrees.Where(o =>
                    _context.Degrees.Find(o.DegreeId) != null
                        ? _context.Degrees.Find(o.DegreeId)!.FamilyId == familyId
                        : false
                ).Select(od => _context.Offers.First(o => o.OfferId == od.OfferId));
            }
            else if (levelId is not null)
            {
                offers = _context.OfferDegrees.Where(o =>
                    _context.Degrees.Find(o.DegreeId) != null
                        ? _context.Degrees.Find(o.DegreeId)!.LevelId == levelId
                        : false
                ).Select(od => _context.Offers.First(o => o.OfferId == od.OfferId));
            }

            // @ Apply other filters
            return offers
                .OrderBy(o => o.OfferId)
                .Where(o => search == null || (o.Name + o.Description).Contains(search))
                .Where(o => companyId == null || o.CompanyId == companyId)
                .Where(o => (minSalary == null || maxSalary == null) || (o.Salary >= minSalary && o.Salary <= maxSalary))
                .Skip(page * size)
                .Take(size);
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

        public Offer Create(Offer offer, int companyId, IEnumerable<int> degrees)
        {
            offer.CompanyId = companyId;
            var change = _context.Offers.Add(offer);
            _context.SaveChanges();

            // Assign degrees
            _context.OfferDegrees.AddRange(degrees.Select(degreeId => new OfferDegree()
            {
                OfferId = change.Entity.OfferId,
                DegreeId = degreeId
            }));
            _context.SaveChanges();

            return change.Entity;
        }

        public Offer? Update(Offer offer, IEnumerable<int>? degrees)
        {
            var existing = _context.Offers.Find(offer.OfferId);
            if (existing is not null)
            {
                existing = EntityPropertyMapper.InjectNonNull(existing, offer);
                // Update degrees if required
                if (degrees is not null && degrees.Count() > 0)
                {
                    _context.OfferDegrees.RemoveRange(_context.OfferDegrees.Where(od => od.OfferId == existing.OfferId));
                    _context.OfferDegrees.AddRange(degrees.Select(degreeId => new OfferDegree()
                    {
                        OfferId = existing.OfferId,
                        DegreeId = degreeId
                    }));
                }

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