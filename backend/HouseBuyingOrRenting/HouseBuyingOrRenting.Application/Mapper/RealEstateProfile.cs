using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class RealEstateProfile : Profile
    {
        public RealEstateProfile()
        {
            CreateMap<RealEstate, RealEstateDto>();
            CreateMap<RealEstateCreateDto, RealEstate>();
            CreateMap<RealEstateUpdateDto, RealEstate>();
        }
    }
}
