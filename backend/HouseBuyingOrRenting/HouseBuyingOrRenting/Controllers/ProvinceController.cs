using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProvinceController : BaseController<Province, ProvinceDto, ProvinceCreateDto, ProvinceUpdateDto>
    {
        public ProvinceController(IProvinceService provinceService) : base(provinceService)
        {
        }
    }
}
