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
            result = result.Where(r => !r.IsDeleted).ToList();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetByProvinceId(Guid? provinceId, int pageSize, int pageNumber, int postType)
        {
            var result = await _realEstateService.GetList(provinceId, pageSize, pageNumber);
            result = result.Where(r => (int)r.Type == postType && !r.IsDeleted).ToList();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("owner/{id}")]
        public async Task<IActionResult> GetByOwner(Guid id)
        {
            var result = await _realEstateService.GetByOwner(id);
            result = result.Where(r => !r.IsDeleted).ToList();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("save-history/{id}")]
        public async Task<IActionResult> GetSaveHistory(Guid id)
        {
            var realEstateIds = await _postSaveService.GetRealEstateIdsByUser(id);
            var result = await _realEstateService.GetByIdsAsync(realEstateIds);
            result = result.Where(r => !r.IsDeleted).ToList();
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

            result = result.GroupBy(r => r.Id).Select(g => g.First()).ToList();

            if ((int) type > 0)
            {
                result = result.Where(r => r.Type == type).ToList();
            }
            result = result.Where(r => !r.IsDeleted).ToList();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> FilterRealEstate(PostType type, string realEstateTypeStr
            , double minPrice, double maxPrice, double minArea, double maxArea)
        {
            var result = await _realEstateService.FilterRealEstate(type, realEstateTypeStr, minPrice, maxPrice, minArea, maxArea);
            result = result.Where(r => !r.IsDeleted).ToList();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("search-by-province/{id}")]
        public async Task<IActionResult> SearchByProvince(Guid id)
        {
            var result = await _realEstateService.GetByProvinceIds(new List<Guid>() { id });
            result = result.Where(r => !r.IsDeleted).ToList();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("search-by-district/{id}")]
        public async Task<IActionResult> SearchByDisctrict(Guid id, int postType)
        {
            var result = await _realEstateService.GetByDisctrictIds(new List<Guid>() { id });
            result = result.Where(r => (int)r.Type == postType).ToList();
            result = result.Where(r => !r.IsDeleted).ToList();
            return StatusCode(StatusCodes.Status200OK, result);
        }


        [HttpPost]
        [Route("accept/{id}")]
        public async Task<IActionResult> AcceptRealEstate(Guid id)
        {
            var result = await _realEstateService.ChangeStatus(id, 1);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost]
        [Route("reject/{id}")]
        public async Task<IActionResult> RejectRealEstate(Guid id)
        {
            var result = await _realEstateService.ChangeStatus(id, 2);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
