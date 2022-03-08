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

        [Route("applicants/{companyId}")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<CompanyDTO>> FindCompanyApplicants(int companyId)
        {
            return Ok(CompanyBL.CountCompanyApplicants(companyId));
        }

        [HttpPut("update-profile")]
        [Authorize(Roles = "Company")]
        public ActionResult<AuthenticatedDTO> Update(UpdateCompanyDTO updateDto)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(CompanyBL.Update(updateDto, username));
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
        public ActionResult<CompanyDTO> FindById()
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(CompanyBL.FindProfile(username));
        }

        [HttpGet("profile/{username}")]
        [AllowAnonymous]
        public ActionResult<PublicCompanyDTO> FindPublicProfile(string username)
        {
            return Ok(CompanyBL.FindPublicProfile(username));
        }

        [HttpDelete("{companyId}")]
        [Authorize]
        public ActionResult<CompanyDTO> Delete(int companyId)
        {
            return Ok(CompanyBL.Delete(companyId));
        }

        [HttpPut("premium")]
        [Authorize(Roles = "Company")]
        public ActionResult<AuthenticatedDTO> Premium()
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            CompanyBL.Premium(username);
            return Ok();
        }


    }
}
