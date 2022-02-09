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
            bool skipExpired,
            int? companyId,
            int? studentId,
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
        // @ Applications
        int ApplicationCount(int studentId);
        IEnumerable<Student> FindOfferApplicants(int offerId);
        ApplicationStatus? FindApplicationStatus(int offerId, int studentId);
        ApplicationStatus? FindApplicantStatus(int studentId, int offerId);
        Application? FindApplication(int offerId, int studentId);
        Application CreateApplication(int offerId, int studentId, ApplicationStatus status);
        Application? DeleteApplication(int offerId, int studentId);
        Application? UpdateApplicationStatus(int offerId, int studentId, ApplicationStatus status);
    }
}
