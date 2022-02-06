using System.ComponentModel.DataAnnotations;

namespace InterSMeet.Core.DTO
{
    public class CheckRestorePasswordDTO
    {
        [Required]
        public string Credential { get; set; } = null!;
        [Required]
        public string RestorePasswordCode { get; set; } = null!;
    }
}
