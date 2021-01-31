using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworkCore.Api.Extensions
{
    using AutoMapper.Configuration;
    using FrameworkCore.Shared.DataModel;
    using FrameworkCore.Shared.Dto;

    public class AutoMappingFile : MapperConfigurationExpression
    {
        public AutoMappingFile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Basic, BasicDto>().ReverseMap();

            CreateMap<GroupUserDto, GroupUser>().ReverseMap();
            CreateMap<GroupUser, GroupUserDto>().ReverseMap();
        }
    }
}
