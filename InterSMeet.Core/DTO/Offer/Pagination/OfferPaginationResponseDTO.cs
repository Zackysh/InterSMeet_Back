
namespace InterSMeet.Core.DTO.Offer
{
    public class OfferPaginationResponseDTO
    {
        public OfferPaginationOptionsDTO Pagination { get; set; } = null!;
        public IEnumerable<object> Offers { get; set; } = null!; // object enables inheritance
    }
}
