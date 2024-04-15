using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImageUrlController : BaseController<ImageUrl, ImageUrlDto, ImageUrlCreateDto, ImageUrlUpdateDto>
    {
        public ImageUrlController(IImageUrlService imageUrlSerivce) : base(imageUrlSerivce)
        {
        }
    }
}
