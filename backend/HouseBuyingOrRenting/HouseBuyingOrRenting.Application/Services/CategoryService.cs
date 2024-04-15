using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class CategoryService : BaseService<Category, CategoryDto, CategoryCreateDto, CategoryUpdateDto>, ICategoryService
    {
        public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
        {
        }

        public override Task<Category> MapEntityCreateDtoToEntity(CategoryCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<CategoryDto> MapEntityToEntityDto(Category entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Category> MapEntityUpdateDtoToEntity(Guid id, CategoryUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
