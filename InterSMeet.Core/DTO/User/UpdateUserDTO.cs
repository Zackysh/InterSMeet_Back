using System.ComponentModel.DataAnnotations;


namespace InterSMeet.Core.DTO
{
    public class UpdateUserDTO
    {
        [MaxLength(40)]
        public string? Username { get; set; }
        [MaxLength(70)]
        public string? FirstName { get; set; }
        [MaxLength(70)]
        public string? LastName { get; set; }
        public int? ProvinceId { get; set; }
        public string? Location { get; set; }
        public int? LanguageId { get; set; }
    }
}
