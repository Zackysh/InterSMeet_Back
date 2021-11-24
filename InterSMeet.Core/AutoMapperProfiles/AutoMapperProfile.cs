using AutoMapper;
using InterSMeet.Core.DTO;
using InterSMeet.DAL.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CreateMap<SignUpDTO, User>();
            // LanguageDTO - Language
            CreateMap<LanguageDTO, Language>();
            CreateMap<Language, LanguageDTO>();
            // UpdateUserDTO - User
            CreateMap<UpdateUserDTO, User>();
            // CreateUserDTO - User
            CreateMap<CreateUserDTO, User>();

        }

    }
}
