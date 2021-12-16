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
            // SignUpDTO - User
            CreateMap<UserSignUpDTO, User>();
            // LanguageDTO - Language
            CreateMap<LanguageDTO, Language>();
            CreateMap<Language, LanguageDTO>();
            // LanguageDTO - Language
            CreateMap<Province, ProvinceDTO>();
            // ImageDTO - Image
            CreateMap<ImageDTO, Image>();
            CreateMap<Image, ImageDTO>();

        }

    }
}
