using AutoMapper;
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
        private readonly IMapper _mapper;

        public ImageUrlService(IImageUrlRepository imageUrlRepository, IMapper mapper) : base(imageUrlRepository)
        {
            _mapper = mapper;
        }

        public async override Task<ImageUrl> MapEntityCreateDtoToEntity(ImageUrlCreateDto entityCreateDto)
        {
            var imageUrl = _mapper.Map<ImageUrl>(entityCreateDto);
            return imageUrl;
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
