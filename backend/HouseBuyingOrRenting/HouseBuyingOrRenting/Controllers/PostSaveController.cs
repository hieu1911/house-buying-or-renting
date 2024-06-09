using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostSaveController : BaseController<PostSave, PostSave, PostSaveCreateDto, PostSave>
    {
        private readonly IPostSaveService _postSaveService;

        public PostSaveController(IPostSaveService postSaveService) : base(postSaveService)
        {
            _postSaveService = postSaveService;
        }

        [HttpGet]
        [Route("user-realestate")]
        public async Task<IActionResult> GetBuyUserIdAndRealEstateId(Guid userId, Guid realEstateId)
        {
            var result = await _postSaveService.GetBuyUserIdAndRealEstateId(userId, realEstateId);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
