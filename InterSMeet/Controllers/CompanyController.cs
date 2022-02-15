using InterSMeet.ApiRest.Utils;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterSMeet.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        internal ICompanyBL CompanyBL { get; set; }

        public CompanyController(ICompanyBL companyBL)
        {
            CompanyBL = companyBL;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<CompanyDTO>> FindAllAdmin()
        {
            return Ok(CompanyBL.FindAllAdmin());
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<PublicCompanyDTO>> FindAll()
        {
            return Ok(CompanyBL.FindAll());
        }

        [HttpGet("profile")]
        [Authorize]
        public ActionResult<StudentDTO> FindById()
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(CompanyBL.FindProfile(username));
        }

        [HttpDelete("{companyId}")]
        [Authorize]
        public ActionResult<CompanyDTO> Delete(int companyId)
        {
            return Ok(CompanyBL.Delete(companyId));
        }
    }
}
