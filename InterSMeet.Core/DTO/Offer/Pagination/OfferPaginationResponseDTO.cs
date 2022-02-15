
namespace InterSMeet.Core.DTO.Offer
{
    public class OfferPaginationResponseDTO
    {
        public OfferPaginationOptionsDTO Pagination { get; set; } = null!;
        public IEnumerable<object> Results { get; set; } = null!; // object enables inheritance
        public int Total { get; set; }
    }
}
