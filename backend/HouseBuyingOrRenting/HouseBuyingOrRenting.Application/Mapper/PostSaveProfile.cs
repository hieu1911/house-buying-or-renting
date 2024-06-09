using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class PostSaveProfile : Profile
    {
        public PostSaveProfile()
        {
            CreateMap<PostSaveCreateDto, PostSave>();
        }
    }
}
