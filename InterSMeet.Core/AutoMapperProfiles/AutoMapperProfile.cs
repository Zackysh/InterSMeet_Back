using AutoMapper;
using InterSMeet.Core.DTO;
using InterSMeet.DAL.Entities;
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
            CreateMap<UserRoleDTO, User>();
            CreateMap<User, UserRoleDTO>();
        }

    }
}
