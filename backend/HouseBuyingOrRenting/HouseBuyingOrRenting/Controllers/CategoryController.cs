﻿using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : BaseController<Category, CategoryDto, CategoryCreateDto, CategoryUpdateDto>
    {
        public CategoryController(ICategoryService categoryService) : base(categoryService)
        {
        }
    }
}