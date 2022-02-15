using AutoMapper;
using InterSMeet.Core.DTO;
using InterSMeet.DAL.Entities;

namespace InterSMeet.Core.MapperProfiles
{
    public class CompanyConverter : ITypeConverter<Company, CompanyDTO>
    {
        public CompanyDTO Convert(Company source, CompanyDTO destination, ResolutionContext context)
        {
            return new()
            {
                UserId = source.User.UserId,
                CompanyId = source.CompanyId,
                Username = source.User.Username,
                Email = source.User.Email,
                FirstName = source.User.FirstName,
                LastName = source.User.LastName,
                ProvinceId = source.User.ProvinceId,
                Location = source.User.Location,
                LanguageId = source.User.LanguageId,
                RoleId = source.User.RoleId,
                Address = source.Address,
                CompanyName = source.CompanyName,
            };
        }
    }
}