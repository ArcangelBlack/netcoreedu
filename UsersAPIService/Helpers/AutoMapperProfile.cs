using AutoMapper;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersAPIService.Dto;

namespace UsersAPIService.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UserDto>();
            CreateMap<UserDto, Users>();
        }
    }
}
