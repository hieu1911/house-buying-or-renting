using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BoardingHouseController : BaseController<BoardingHouse, BoardingHouseDto, BoardingHouseCreateDto, BoardingHouseUpdateDto>
    {
        private readonly IBoardingHouseService _boardingHouseService;

        public BoardingHouseController(IBoardingHouseService boardingHouseService) : base(boardingHouseService)
        {
            _boardingHouseService = boardingHouseService;
        }

        [HttpGet]
        [Route("RealEstate/{realEstateId}")]
        public async Task<IActionResult> GetByRealEstateId(Guid realEstateId)
        {
            var result = await _boardingHouseService.GetByRealEstateId(realEstateId);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
