using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class ApartmentProfile : Profile
    {
        public ApartmentProfile()
        {
            CreateMap<Apartment, ApartmentDto>();
            CreateMap<ApartmentCreateDto, Apartment>();
            CreateMap<ApartmentUpdateDto, Apartment>();
        }
    }
}
