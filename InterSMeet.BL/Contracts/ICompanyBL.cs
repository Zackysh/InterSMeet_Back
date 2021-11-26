using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.BLL.Contracts
{
    public interface ICompanyBL
    {
        IEnumerable<CompanyDTO> FindAll();
        CompanyDTO FindProfile(string username);
        CompanyDTO Delete(int companyId);
    }
}
