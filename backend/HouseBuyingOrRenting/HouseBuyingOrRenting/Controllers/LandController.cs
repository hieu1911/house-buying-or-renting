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
        public LandController(ILandService districtService) : base(districtService)
        {
        }
    }
}
