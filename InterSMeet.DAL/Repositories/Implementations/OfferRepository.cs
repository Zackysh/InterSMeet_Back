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
                offers = FindOffersByDegree((int)degreeId);
            else if (familyId is not null)
                offers = FindOffersByFamily((int)familyId);
            else if (levelId is not null)
                offers = FindOffersByLevel((int)levelId);

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

        public IEnumerable<Student> FindOfferApplicants(int offerId)
        {
            // TODO implement when Application entity is ready
            var students = _context.Students
            .Join(
                _context.Users,
                s => s.StudentId,
                u => u.UserId,
                (student, user) => new { student, user }
            );

            foreach (var std in students)
                std.student.User = std.user;

            return students.Select(full => full.student);
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

        /// ======================================================================================================================
        /// @ Private Methods
        /// ======================================================================================================================

        private IEnumerable<Offer> FindOffersByDegree(int degreeId)
        {
            return _context.Offers
                .Join(
                    _context.OfferDegrees,
                    o => o.OfferId,
                    od => od.OfferId,
                    (offer, offerDegree) => new { offer, offerDegree }
                )
                .Where(full => full.offerDegree.DegreeId == degreeId)
                .Select(full => full.offer); ;
        }

        private IEnumerable<Offer> FindOffersByFamily(int familyId)
        {
            return _context.Offers
                .Join(
                    _context.OfferDegrees,
                    o => o.OfferId,
                    od => od.OfferId,
                    (offer, offerDegree) => new { offer, offerDegree }
                )
                .Join(
                    _context.Degrees,
                    c => c.offerDegree.DegreeId,
                    d => d.DegreeId,
                    (combined, degree) => new { combined.offer, combined.offerDegree, degree }
                )
                .Where(full => full.degree.FamilyId == familyId)
                .Select(full => full.offer).AsEnumerable();
        }

        private IEnumerable<Offer> FindOffersByLevel(int levelId)
        {
            return _context.Offers
                .Join(
                    _context.OfferDegrees,
                    o => o.OfferId,
                    od => od.OfferId,
                    (offer, offerDegree) => new { offer, offerDegree }
                )
                .Join(
                    _context.Degrees,
                    c => c.offerDegree.DegreeId,
                    d => d.DegreeId,
                    (combined, degree) => new { combined.offer, combined.offerDegree, degree }
                )
                .Where(full => full.degree.LevelId == levelId)
                .Select(full => full.offer);
        }


    }
}