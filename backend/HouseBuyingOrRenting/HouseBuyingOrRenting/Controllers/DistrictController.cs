using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DistrictController : BaseController<District, DistrictDto, DistrictCreateDto, DistrictUpdateDto>
    {
        private readonly IDistrictService _districtService;

        public DistrictController(IDistrictService districtService) : base(districtService)
        {
            _districtService = districtService;
        }

        [HttpGet]
        [Route("by-province-id/{provinceId}")]
        public async Task<IActionResult> GetDistrictsByProvinceId(Guid provinceId)
        {
            var result = await _districtService.GetDistrictsByProvinceId(provinceId);

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
