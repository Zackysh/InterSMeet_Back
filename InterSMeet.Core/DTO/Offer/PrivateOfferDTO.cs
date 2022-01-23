namespace InterSMeet.Core.DTO.Offer
{
    public class PrivateOfferDTO : OfferDTO
    {
        public IEnumerable<ApplicantDTO> Applicants { get; set; } = null!;
    }
}
