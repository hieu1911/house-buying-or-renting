using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HouseController : BaseController<House, HouseDto, HouseCreateDto, HouseUpdateDto>
    {
        private readonly IHouseService _houseService;

        public HouseController(IHouseService houseService) : base(houseService)
        {
            _houseService = houseService;
        }

        [HttpGet]
        [Route("RealEstate/{realEstateId}")]
        public async Task<IActionResult> GetByRealEstateId(Guid realEstateId)
        {
            var result = await _houseService.GetByRealEstateId(realEstateId);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
