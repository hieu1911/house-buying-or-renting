using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class ImageUrlService : BaseService<ImageUrl, ImageUrlDto, ImageUrlCreateDto, ImageUrlUpdateDto>, IImageUrlService
    {
        public ImageUrlService(IImageUrlRepository imageUrlRepository) : base(imageUrlRepository)
        {
        }

        public override Task<ImageUrl> MapEntityCreateDtoToEntity(ImageUrlCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<ImageUrlDto> MapEntityToEntityDto(ImageUrl entity)
        {
            throw new NotImplementedException();
        }

        public override Task<ImageUrl> MapEntityUpdateDtoToEntity(Guid id, ImageUrlUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
