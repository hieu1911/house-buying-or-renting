using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LandController : BaseController<Land, LandDto, LandCreateDto, LandUpdateDto>
    {
        private readonly ILandService _landService;

        public LandController(ILandService landService) : base(landService)
        {
            _landService = landService;
        }

        [HttpGet]
        [Route("RealEstate/{realEstateId}")]
        public async Task<IActionResult> GetByRealEstateId(Guid realEstateId)
        {
            var result = await _landService.GetByRealEstateId(realEstateId);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
