using AutoMapper;
using InterSMeet.Core.DTO;
using InterSMeet.DAL.Entities;

namespace InterSMeet.Core.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            // UserDTO - User
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            // Student - StudentDTO
            CreateMap<Student, StudentDTO>().ConvertUsing(new StudentConverter());
            // Company - CompanyDTO
            CreateMap<Company, CompanyDTO>().ConvertUsing(new CompanyConverter());
            // SignUpDTO - User
            CreateMap<UserSignUpDTO, User>();
            // LanguageDTO - Language
            CreateMap<LanguageDTO, Language>();
            CreateMap<Language, LanguageDTO>();
            // Province - ProvinceDTO
            CreateMap<Province, ProvinceDTO>();
            // Degree - DegreeDTO
            CreateMap<Degree, DegreeDTO>();
            // ImageDTO - Image
            CreateMap<ImageDTO, Image>();
            CreateMap<Image, ImageDTO>();

        }

    }
}
