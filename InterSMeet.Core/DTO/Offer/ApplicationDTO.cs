using InterSMeet.DAL.Entities;

namespace InterSMeet.Core.DTO.Offer
{
    public class ApplicationDTO : OfferDTO
    {
        public string Status { get; set; } = null!;

        public static ApplicationDTO FromOfferDTO(OfferDTO offerDto, ApplicationStatus status)
        {
            return new()
            {
                OfferId = offerDto.OfferId,
                Name = offerDto.Name,
                Description = offerDto.Description,
                Schedule = offerDto.Schedule,
                Salary = offerDto.Salary,
                CompanyId = offerDto.CompanyId,
                Status = status.ToString(),
            };
        }
    }
}
