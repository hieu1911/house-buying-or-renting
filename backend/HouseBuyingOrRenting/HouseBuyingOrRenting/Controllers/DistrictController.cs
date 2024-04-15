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
        public DistrictController(IDistrictService districtService) : base(districtService)
        {
        }
    }
}
