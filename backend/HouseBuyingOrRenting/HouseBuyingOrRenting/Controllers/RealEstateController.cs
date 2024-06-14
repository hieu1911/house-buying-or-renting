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
        private readonly IRealEstateService _realEstateService;

        private readonly IPostSaveService _postSaveService;

        private readonly IProvinceService _provinceService;

        private readonly IDistrictService _districtService;

        public RealEstateController(IRealEstateService realEstateService, 
            IPostSaveService postSaveService,
            IProvinceService provinceService,
            IDistrictService districtService) : base(realEstateService)
        {
            _realEstateService = realEstateService;
            _postSaveService = postSaveService;
            _provinceService = provinceService;
            _districtService = districtService;
        }

        [HttpGet]
        [Route("rent")]
        public async Task<IActionResult> GetAllRentRealEstate()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("carousel")]
        public async Task<IActionResult> GetForCarousel()
        {
            var result = await _realEstateService.GetRealEstateForCarousel();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetByProvinceId(Guid? provinceId, int pageSize, int pageNumber)
        {
            var result = await _realEstateService.GetList(provinceId, pageSize, pageNumber);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("owner/{id}")]
        public async Task<IActionResult> GetByOwner(Guid id)
        {
            var result = await _realEstateService.GetByOwner(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("save-history/{id}")]
        public async Task<IActionResult> GetSaveHistory(Guid id)
        {
            var realEstateIds = await _postSaveService.GetRealEstateIdsByUser(id);
            var result = await _realEstateService.GetByIdsAsync(realEstateIds);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("search-by-key")]
        public async Task<IActionResult> SearchRealEstateBuyKey(string value, PostType type)
        {
            var realEstateByTitle = await _realEstateService.SearchByTitle(value);
            var provinces = await _provinceService.SearchByName(value);
            var districts = await _districtService.SearchByName(value);

            var realEstateByProvince = await _realEstateService.GetByProvinceIds(provinces.Select(p => p.Id).ToList());
            var realEstateByDistrict = await _realEstateService.GetByDisctrictIds(districts.Select(d => d.Id).ToList());

            var result = new List<RealEstateDto>();
            result.AddRange(realEstateByTitle);
            result.AddRange(realEstateByProvince);
            result.AddRange(realEstateByDistrict);

            if ((int) type > 0)
            {
                result = result.Where(r => r.Type == type).ToList();
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> FilterRealEstate(PostType type, string realEstateTypeStr
            , double minPrice, double maxPrice, double minArea, double maxArea)
        {
            var result = await _realEstateService.FilterRealEstate(type, realEstateTypeStr, minPrice, maxPrice, minArea, maxArea);

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
