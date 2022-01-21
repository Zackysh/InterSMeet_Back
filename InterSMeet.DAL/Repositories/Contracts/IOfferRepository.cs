using InterSMeet.DAL.Base;
using InterSMeet.DAL.Entities;

namespace InterSMeet.DAL.Repositories.Contracts
{
    public interface IOfferRepository : IRepository<Offer>
    {
        IEnumerable<Offer> Pagination(
            int page,
            int size,
            string? search,
            int? companyId,
            int? degreeId,
            int? familyId,
            int? levelId,
            double? minSalary,
            double? maxSalary);
        IEnumerable<Offer> FindAll();
        IEnumerable<Offer> FindCompanyOffers(int companyId);
        Offer? FindById(int offerId);
        Offer Create(Offer offer, int companyId, IEnumerable<int> degrees);
        Offer? Update(Offer offer, IEnumerable<int>? degrees);
        Offer? Delete(int offerId);
    }
}
