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
        public ApartmentController(IApartmentService districtService) : base(districtService)
        {
        }
    }
}
