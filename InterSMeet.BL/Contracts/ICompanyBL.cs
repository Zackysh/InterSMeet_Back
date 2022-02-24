using InterSMeet.Core.DTO;

namespace InterSMeet.BLL.Contracts
{
    public interface ICompanyBL
    {
        IEnumerable<PublicCompanyDTO> FindAll();
        IEnumerable<CompanyDTO> FindAllAdmin();
        CompanyDTO FindProfile(string username);
        CompanyDTO Delete(int companyId);
        AuthenticatedDTO Update(UpdateCompanyDTO updateDto, string username);
        int CountCompanyApplicants(int companyId);
        PublicCompanyDTO FindPublicProfile(string username);
    }
}
