using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class HouseService : BaseService<House, HouseDto, HouseCreateDto, HouseUpdateDto>, IHouseService
    {
        private readonly IImageUrlService _imageUrlService;

        private readonly IMapper _mapper;

        public HouseService(IHouseRepository realEstateRepository, IImageUrlService imageUrlService, IMapper mapper) : base(realEstateRepository)
        {
            _imageUrlService = imageUrlService;
            _mapper = mapper;
        }

        public async override Task<int> InsertAsync(HouseCreateDto entityCreateDto)
        {

            var house = await MapEntityCreateDtoToEntity(entityCreateDto);
            house.CreatedDate = DateTime.Now;
            house.CreatedName = "";
            house.Id = Guid.NewGuid();

            var imageUrlsCreateDto = entityCreateDto.ImageUrlsCreateDto.Select(imageUrlCreateDto =>
            {
                imageUrlCreateDto.RealEstateId = house.Id;
                return imageUrlCreateDto;
            }).ToList();

            var result = await BaseRepository.InsertAsync(house);
            await _imageUrlService.InsertMultiAsync(imageUrlsCreateDto);

            return result;
        }

        public async override Task<House> MapEntityCreateDtoToEntity(HouseCreateDto entityCreateDto)
        {
            var house = _mapper.Map<House>(entityCreateDto);
            return house;
        }

        public override Task<HouseDto> MapEntityToEntityDto(House entity)
        {
            throw new NotImplementedException();
        }

        public override Task<House> MapEntityUpdateDtoToEntity(Guid id, HouseUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
