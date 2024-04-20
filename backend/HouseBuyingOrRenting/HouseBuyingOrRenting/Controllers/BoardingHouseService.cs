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
        public BoardingHouseController(IBoardingHouseService provinceService) : base(provinceService)
        {
        }
    }
}
