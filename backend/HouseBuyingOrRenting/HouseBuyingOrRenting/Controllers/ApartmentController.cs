using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApartmentController : BaseController<Apartment, ApartmentDto, ApartmentCreateDto, ApartmentUpdateDto>
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentController(IApartmentService apartmentService) : base(apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [HttpGet]
        [Route("RealEstate/{realEstateId}")]
        public async Task<IActionResult> GetByRealEstateId(Guid realEstateId)
        {
            var result = await _apartmentService.GetByRealEstateId(realEstateId);
            return StatusCode(StatusCodes.Status201Created, result.RealEstateDto.IsDeleted ? null : result);
        }
    }
}
