namespace InterSMeet.Core.DTO.Offer
{
    public class PrivateOfferDTO : OfferDTO
    {
        public IEnumerable<StudentDTO> Applicants { get; set; } = null!;
    }
}
