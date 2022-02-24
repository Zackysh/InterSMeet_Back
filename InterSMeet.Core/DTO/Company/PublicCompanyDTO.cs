namespace InterSMeet.Core.DTO
{
    public class PublicCompanyDTO
    {
        public int CompanyId { get; set; }
        public string Address { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Web { get; set; }
        public string? BackgroundUrl { get; set; }
        public string? LogoUrl { get; set; }

        // @ User data
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int ProvinceId { get; set; }
        public string Location { get; set; } = null!;
        

        public static PublicCompanyDTO FronCompanyDto(CompanyDTO companyDTO)
        {
            return new()
            {
                CompanyId = companyDTO.CompanyId,
                Address = companyDTO.Address,
                CompanyName = companyDTO.CompanyName,
                Description = companyDTO.Description,
                Web = companyDTO.Web,
                BackgroundUrl = companyDTO.BackgroundUrl,
                LogoUrl = companyDTO.LogoUrl,
                // @ User data
                Email = companyDTO.Email,
                Username = companyDTO.Username,
                ProvinceId = companyDTO.ProvinceId,
                Location = companyDTO.Location,
            };
        }
    }

}
