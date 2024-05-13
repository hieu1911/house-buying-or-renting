using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class LandService : BaseService<Land, LandDto, LandCreateDto, LandUpdateDto>, ILandService
    {
        private readonly IImageUrlService _imageUrlService;

        public LandService(ILandRepository districtRepository, IImageUrlService imageUrlService) : base(districtRepository)
        {
            _imageUrlService = imageUrlService;
        }

        public async override Task<int> InsertAsync(LandCreateDto entityCreateDto)
        {

            var land = await MapEntityCreateDtoToEntity(entityCreateDto);
            land.CreatedDate = DateTime.Now;
            land.CreatedName = "";
            land.Id = Guid.NewGuid();

            var imageUrlsCreateDto = entityCreateDto.ImageUrlsCreateDto.Select(imageUrlCreateDto =>
            {
                imageUrlCreateDto.RealEstateId = land.Id;
                return imageUrlCreateDto;
            }).ToList();

            var result = await BaseRepository.InsertAsync(land);
            await _imageUrlService.InsertMultiAsync(imageUrlsCreateDto);

            return result;
        }

        public override Task<Land> MapEntityCreateDtoToEntity(LandCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<LandDto> MapEntityToEntityDto(Land entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Land> MapEntityUpdateDtoToEntity(Guid id, LandUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
