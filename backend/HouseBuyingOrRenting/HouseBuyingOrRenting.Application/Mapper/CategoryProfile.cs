using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
