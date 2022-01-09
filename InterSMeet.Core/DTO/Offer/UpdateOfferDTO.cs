

using System.ComponentModel.DataAnnotations;

namespace InterSMeet.Core.DTO
{
    public class UpdateOfferDTO
    {
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string? Schedule { get; set; }
        [Range(0, 15000)]
        public double? Salary { get; set; }
    }
}
