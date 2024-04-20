using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class LandProfile : Profile
    {
        public LandProfile()
        {
            CreateMap<Land, LandDto>();
            CreateMap<LandCreateDto, Land>();
            CreateMap<LandUpdateDto, Land>();
        }
    }
}
