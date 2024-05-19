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

        public override async Task<ImageUrl> MapEntityCreateDtoToEntity(ImageUrlCreateDto entityCreateDto)
        {
            var imageUrl = _mapper.Map<ImageUrl>(entityCreateDto);
            return imageUrl;
        }

        public override async Task<ImageUrlDto> MapEntityToEntityDto(ImageUrl entity)
        {
            var imageUrlDto = _mapper.Map<ImageUrlDto>(entity);
            return imageUrlDto;
        }

        public override async Task<ImageUrl> MapEntityUpdateDtoToEntity(Guid id, ImageUrlUpdateDto entityUpdateDto)
        {
            var imageUrl = _mapper.Map<ImageUrl>(entityUpdateDto);
            imageUrl.Id = id;
            return imageUrl;
        }
    }
}
