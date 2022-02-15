namespace InterSMeet.Core.DTO
{
    public class UpdateCompanyDTO
    {
        public UpdateUserDTO? UpdateUserDto { get; set; } = null!;
        public DateTime? Address { get; set; }
        public string? CompanyName { get; set; }
    }
}
