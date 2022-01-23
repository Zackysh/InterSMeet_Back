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

        // GET api/offers/pagination
        [HttpGet("pagination")]
        [Authorize]
        public ActionResult<OfferPaginationResponseDTO> Pagination(OfferPaginationOptionsDTO pagination)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return OfferBL.Pagination(pagination, username);
        }

        // GET api/offers
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<OfferDTO>> FindAll()
        {
            return Ok(OfferBL.FindAll());
        }

        // GET api/offers/:offerId
        [HttpGet("{offerId}")]
        [Authorize]
        public ActionResult<OfferDTO> FindById(int offerId)
        {
            return Ok(OfferBL.FindById(offerId));
        }

        // GET api/offers/my-offers
        [HttpGet("my-offers")]
        [Authorize(Roles = "Company")]
        public ActionResult<IEnumerable<PrivateOfferDTO>> FindCompanyOffers()
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(OfferBL.FindCompanyOffers(username));
        }

        // POST api/offers
        [HttpPost]
        [Authorize(Roles = "Company")]
        public ActionResult<OfferDTO> Create(CreateOfferDTO createOfferDTO)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(OfferBL.Create(createOfferDTO, username));
        }

        // PUT api/offers/:offerId
        [HttpPut("{offerId}")]
        [Authorize(Roles = "Company")]
        public ActionResult<OfferDTO> Update(UpdateOfferDTO updateOfferDTO, int offerId)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);

            return Ok(OfferBL.Update(updateOfferDTO, username, offerId));
        }

        // DELETE api/offers/:offerId
        [HttpDelete("{offerId}")]
        [Authorize(Roles = "Company")]
        public ActionResult<OfferDTO> Delete(int offerId)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(OfferBL.Delete(offerId, username));
        }

        // @ Applications

        // POST api/offers/applications/:offerId
        [HttpPost("applications/{offerId}")]
        [Authorize(Roles = "Student")]
        public ActionResult<ApplicationDTO> CreateApplication(int offerId)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(OfferBL.CreateApplication(offerId, username));
        }

        // DELETE api/offers/applications/:offerId
        [HttpDelete("applications/{offerId}")]
        [Authorize(Roles = "Student")]
        public ActionResult<ApplicationDTO> DeleteApplication(int offerId)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(OfferBL.DeleteApplication(offerId, username));
        }

        // POST api/offers/applications/accept/{offerId}/{studentId}
        [HttpPut("applications/update-status/{offerId}/{studentId}")]
        [Authorize(Roles = "Company")]
        public ActionResult<ApplicantDTO> DeleteApplication(int offerId, int studentId, ApplicationStatusDTO status)
        {
            var username = ControllerUtils.GetUserIdentity(HttpContext);
            return Ok(OfferBL.UpdateApplicationStatus(offerId, studentId, username, status));
        }
    }
}
