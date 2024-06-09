using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RealEstateController : BaseController<RealEstate, RealEstateDto, RealEstateCreateDto, RealEstateUpdateDto>
    {
        private readonly IRealEstateService _realEstateService;

        private readonly IPostSaveService _postSaveService;

        public RealEstateController(IRealEstateService realEstateService, IPostSaveService postSaveService) : base(realEstateService)
        {
            _realEstateService = realEstateService;
            _postSaveService = postSaveService;
        }

        [HttpGet]
        [Route("rent")]
        public async Task<IActionResult> GetAllRentRealEstate()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("carousel")]
        public async Task<IActionResult> GetForCarousel()
        {
            var result = await _realEstateService.GetRealEstateForCarousel();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetByProvinceId(Guid? provinceId, int pageSize, int pageNumber)
        {
            var result = await _realEstateService.GetList(provinceId, pageSize, pageNumber);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("owner/{id}")]
        public async Task<IActionResult> GetByOwner(Guid id)
        {
            var result = await _realEstateService.GetByOwner(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("save-history/{id}")]
        public async Task<IActionResult> GetSaveHistory(Guid id)
        {
            var realEstateIds = await _postSaveService.GetRealEstateIdsByUser(id);
            var result = await _realEstateService.GetByIdsAsync(realEstateIds);

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
