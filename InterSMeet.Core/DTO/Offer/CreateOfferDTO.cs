

using System.ComponentModel.DataAnnotations;

namespace InterSMeet.Core.DTO.Offer
{
    public class CreateOfferDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public IEnumerable<int> Degrees { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public string? Schedule { get; set; }
        [Required]
        [Range(0, 15000)]
        public double Salary { get; set; }
    }
}
