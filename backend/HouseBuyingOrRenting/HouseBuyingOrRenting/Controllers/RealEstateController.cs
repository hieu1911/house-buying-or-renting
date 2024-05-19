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

        public RealEstateController(IRealEstateService realEstateService) : base(realEstateService)
        {
            _realEstateService = realEstateService;
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
    }
}
