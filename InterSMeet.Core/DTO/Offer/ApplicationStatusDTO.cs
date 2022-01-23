

using System.ComponentModel.DataAnnotations;

namespace InterSMeet.Core.DTO.Offer
{
    public class ApplicationStatusDTO
    {
        [Required]
        public string Status { get; set; } = null!;
    }
}
