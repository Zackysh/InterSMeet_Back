using InterSMeet.DAL.Entities;

namespace InterSMeet.Core.DTO.Offer
{
    public class ApplicationDTO : OfferDTO
    {
        public int Status { get; set; }

        public static ApplicationDTO FromOfferDTO(OfferDTO offerDto, ApplicationStatus status)
        {
            int st = 0;
            switch(status)
            {
                case ApplicationStatus.Accepted:
                    st = 1;
                    break;
                case ApplicationStatus.Denied:
                    st = -1;
                    break;
            }
            return new()
            {
                OfferId = offerDto.OfferId,
                Name = offerDto.Name,
                Description = offerDto.Description,
                Schedule = offerDto.Schedule,
                Salary = offerDto.Salary,
                CompanyId = offerDto.CompanyId,
                Status = st,
            };
        }
    }
}
