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

        private readonly IRealEstateService _realEstateService;

        public HouseController(IHouseService houseService, IRealEstateService realEstateService) : base(houseService)
        {
            _houseService = houseService;
            _realEstateService = realEstateService;
        }

        [HttpGet]
        [Route("RealEstate/{realEstateId}")]
        public async Task<IActionResult> GetByRealEstateId(Guid realEstateId)
        {
            var result = await _houseService.GetByRealEstateId(realEstateId);
            return StatusCode(StatusCodes.Status200OK, result.RealEstateDto.IsDeleted ? null : result);
        }

        [HttpPut]
        [Route("{id}")]
        public override async Task<IActionResult> UpdateAsync(Guid id, HouseUpdateDto entityUpdateDto)
        {
            await _realEstateService.UpdateAsync(entityUpdateDto.RealEstateId, entityUpdateDto.RealEstateUpdateDto);
            return await base.UpdateAsync(id, entityUpdateDto);
        }
    }
}
