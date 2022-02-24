namespace InterSMeet.Core.DTO
{
    public class CompanyDTO : UserDTO
    {
        public int CompanyId { get; set; }
        public string Address { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Web { get; set; }
        public string? BackgroundUrl { get; set; }
        public string? LogoUrl { get; set; }
    }
}
