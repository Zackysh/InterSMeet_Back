

using System.ComponentModel.DataAnnotations;

namespace InterSMeet.Core.DTO.Offer
{
    public class OfferDTO
    {
        public int OfferId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Schedule { get; set; }
        public double Salary { get; set; }
        public int CompanyId { get; set; }
    }
}
