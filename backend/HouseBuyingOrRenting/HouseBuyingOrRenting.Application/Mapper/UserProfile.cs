using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
