using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class ProvinceProfile : Profile
    {
        public ProvinceProfile() 
        {
            CreateMap<Province, ProvinceDto>();
            CreateMap<ProvinceCreateDto, Province>();
            CreateMap<ProvinceUpdateDto, Province>();
        }
    }
}
