using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    public class BaseController<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : ControllerBase 
        where TEntity : BaseEntity where TEntityDto: class where TEntityCreateDto : class where TEntityUpdateDto: class
    {
        protected readonly IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> BaseService;

        public BaseController(IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService)
        {
            BaseService = baseService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await BaseService.GetAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        //[HttpPost]
        //[Route("list-ids")]
        //public async Task<IActionResult> GetByListIds(List<Guid> ids)
        //{
        //    var result = await BaseService.GetByListIdAsync(ids);
        //    return StatusCode(StatusCodes.Status200OK, result);
        //}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            var result = await BaseService.InsertAsync(entityCreateDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            var result = await BaseService.UpdateAsync(id, entityUpdateDto);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await BaseService.DeleteAsync(id);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        //[HttpDelete]
        //public async Task<IActionResult> DeleteMultiAsync(List<Guid> ids)
        //{
        //    var result = await BaseService.DeleteMultiAsync(ids);

        //    return StatusCode(StatusCodes.Status200OK, result);
        //}
    }
}
