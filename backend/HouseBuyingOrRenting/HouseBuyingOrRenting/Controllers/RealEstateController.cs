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
        public RealEstateController(IRealEstateService realEstateService) : base(realEstateService)
        {
        }
    }
}
