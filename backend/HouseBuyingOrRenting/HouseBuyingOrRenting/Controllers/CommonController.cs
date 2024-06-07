using HouseBuyingOrRenting.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IProvinceService _provinceService;
        
        private readonly IDistrictService _districtService;

        public CommonController(IProvinceService provinceService, IDistrictService districtService)
        {
            _provinceService = provinceService;
            _districtService = districtService;
        }

        [HttpGet]
        [Route("address")]
        public async Task<IActionResult> GetAddressName()
        {
            var provincesName = await _provinceService.GetProinvcesName();
            var districtsName = await _districtService.GetDistrictsName();

            var result = provincesName.Concat(districtsName);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
