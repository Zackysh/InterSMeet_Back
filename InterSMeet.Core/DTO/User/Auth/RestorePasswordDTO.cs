using System.ComponentModel.DataAnnotations;

namespace InterSMeet.Core.DTO
{
    public class RestorePasswordDTO
    {
        [Required]
        public string Credential { get; set; } = null!;
        [Required]
        [MaxLength(40)]
        [MinLength(6)]
        public string NewPassword { get; set; } = null!;
        [Required]
        public string RestorePasswordCode { get; set; } = null!;
    }
}
