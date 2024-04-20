using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HouseController : BaseController<House, HouseDto, HouseCreateDto, HouseUpdateDto>
    {
        public HouseController(IHouseService houseService) : base(houseService)
        {
        }
    }
}
