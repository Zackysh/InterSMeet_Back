using InterSMeet.ApiRest.Utils;
using InterSMeet.BLL.Contracts;
using InterSMeet.Core.DTO;
using InterSMeet.Core.DTO.Offer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterSMeet.Controllers
{
    [Route("api/offers")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        internal IOfferBL OfferBL { get; set; }

        public OfferController(IOfferBL offerBL)
        {
            OfferBL = offerBL;
        }

        /// <summary>
        ///     This method offer deep control over offer search and pagination.
        ///     Main features:
        ///         - Filters & Search options. 
        ///         - Students can retrieve their applications.
        ///         - Companies can retrieve their offers with additional applicant data.
        ///     Look at OfferPaginationOptionsDTO for more information.
        /// </summary>
        [HttpGet("pagination")]
        [Authorize]
        public ActionResult<OfferPaginationResponseDTO> Pagination([FromQuery]OfferPaginationOptionsDTO paginationOptions)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return OfferBL.Pagination(paginationOptions, username);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<OfferDTO>> FindAll()
        {
            return Ok(OfferBL.FindAll());
        }

        [HttpGet("{offerId}")]
        [Authorize]
        public ActionResult<OfferDTO> FindById(int offerId)
        {
            return Ok(OfferBL.FindById(offerId));
        }

        [HttpGet("my-offers")]
        [Authorize(Roles = "Company")]
        public ActionResult<IEnumerable<PrivateOfferDTO>> FindCompanyOffers()
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(OfferBL.FindCompanyOffers(username));
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        public ActionResult<OfferDTO> Create(CreateOfferDTO createOfferDTO)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(OfferBL.Create(createOfferDTO, username));
        }

        [HttpPut("{offerId}")]
        [Authorize(Roles = "Company")]
        public ActionResult<OfferDTO> Update(UpdateOfferDTO updateOfferDTO, int offerId)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);

            return Ok(OfferBL.Update(updateOfferDTO, username, offerId));
        }

        [HttpDelete("{offerId}")]
        [Authorize(Roles = "Company")]
        public ActionResult<OfferDTO> Delete(int offerId)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(OfferBL.Delete(offerId, username));
        }

        // @ Applications

        /// <summary> Students can create applications to offers </summary>
        [HttpPost("applications/{offerId}")]
        [Authorize(Roles = "Student")]
        public ActionResult<ApplicationDTO> CreateApplication(int offerId)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(OfferBL.CreateApplication(offerId, username));
        }

        /// <summary> Students can delete its own applications </summary>
        [HttpDelete("applications/{offerId}")]
        [Authorize(Roles = "Student")]
        public ActionResult<ApplicationDTO> DeleteApplication(int offerId)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(OfferBL.DeleteApplication(offerId, username));
        }

        /// <summary> Companies can update applications status </summary>
        [HttpPut("applications/update-status/{offerId}/{studentId}")]
        [Authorize(Roles = "Company")]
        public ActionResult<ApplicantDTO> DeleteApplication(int offerId, int studentId, ApplicationStatusDTO status)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(OfferBL.UpdateApplicationStatus(offerId, studentId, username, status));
        }
    }
}
