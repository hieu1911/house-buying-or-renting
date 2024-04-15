using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class ImageUrlProfile : Profile
    {
        public ImageUrlProfile() 
        {
            CreateMap<ImageUrl, ImageUrlDto>();
            CreateMap<ImageUrlCreateDto, ImageUrl>();
            CreateMap<ImageUrlUpdateDto, ImageUrl>();
        }
    }
}
