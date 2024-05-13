using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class BoardingHouseService : BaseService<BoardingHouse, BoardingHouseDto, BoardingHouseCreateDto, BoardingHouseUpdateDto>, IBoardingHouseService
    {
        private readonly IImageUrlService _imageUrlService;

        public BoardingHouseService(IBoardingHouseRepository districtRepository, IImageUrlService imageUrlService) : base(districtRepository)
        {
            _imageUrlService = imageUrlService;
        }

        public async override Task<int> InsertAsync(BoardingHouseCreateDto entityCreateDto)
        {

            var boardingHouse = await MapEntityCreateDtoToEntity(entityCreateDto);
            boardingHouse.CreatedDate = DateTime.Now;
            boardingHouse.CreatedName = "";
            boardingHouse.Id = Guid.NewGuid();

            var imageUrlsCreateDto = entityCreateDto.ImageUrlsCreateDto.Select(imageUrlCreateDto =>
            {
                imageUrlCreateDto.RealEstateId = boardingHouse.Id;
                return imageUrlCreateDto;
            }).ToList();

            var result = await BaseRepository.InsertAsync(boardingHouse);
            await _imageUrlService.InsertMultiAsync(imageUrlsCreateDto);

            return result;
        }

        public override Task<BoardingHouse> MapEntityCreateDtoToEntity(BoardingHouseCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<BoardingHouseDto> MapEntityToEntityDto(BoardingHouse entity)
        {
            throw new NotImplementedException();
        }

        public override Task<BoardingHouse> MapEntityUpdateDtoToEntity(Guid id, BoardingHouseUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
