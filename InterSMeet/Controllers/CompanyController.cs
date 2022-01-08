using InterSMeet.ApiRest.Utils;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET api/companies
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<CompanyDTO>> FindAll()
        {
            return Ok(CompanyBL.FindAll());
        }

        // GET api/students/:companyId
        [HttpGet("profile")]
        [Authorize]
        public ActionResult<StudentDTO> FindById()
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(CompanyBL.FindProfile(username));
        }

        // DELETE api/companies/:companyId
        [HttpDelete("{companyId}")]
        [Authorize]
        public ActionResult<CompanyDTO> Delete(int companyId)
        {
            return Ok(CompanyBL.Delete(companyId));
        }
    }
}
