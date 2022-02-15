using AutoMapper;
using InterSMeet.Core.DTO;
using InterSMeet.Core.DTO.Offer;
using InterSMeet.DAL.Entities;

namespace InterSMeet.Core.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            // @ User
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<UserSignUpDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            // @ Student
            CreateMap<Student, StudentDTO>().ConvertUsing(new StudentConverter());
            CreateMap<UpdateStudentDTO, Student>();
            // @ Company
            CreateMap<Company, CompanyDTO>().ConvertUsing(new CompanyConverter());
            CreateMap<Company, PublicCompanyDTO>();
            CreateMap<UpdateCompanyDTO, Company>();
            // @ Offer
            CreateMap<Offer, OfferDTO>();
            CreateMap<UpdateOfferDTO, Offer>();
            CreateMap<CreateOfferDTO, Offer>();
            CreateMap<Offer, PrivateOfferDTO>();
            CreateMap<Offer, PublicOfferDTO>();
            CreateMap<Offer, ApplicationDTO>();
            CreateMap<CreateOfferDTO, Offer>();
            // @ Degree
            CreateMap<Degree, DegreeDTO>();
            CreateMap<Family, FamilyDTO>();
            CreateMap<Level, LevelDTO>();
            // @ Language
            CreateMap<LanguageDTO, Language>();
            CreateMap<Language, LanguageDTO>();
            // @ Province
            CreateMap<Province, ProvinceDTO>();
            // @ ImageDTO
            CreateMap<ImageDTO, Image>();
            CreateMap<Image, ImageDTO>();

        }

    }
}
