namespace InterSMeet.Core.DTO
{
    public class UpdateCompanyDTO
    {
        public UpdateUserDTO? UpdateUserDto { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? CompanyName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Web { get; set; }
        public string? BackgroundUrl { get; set; }
        public string? LogoUrl { get; set; }
    }
}
