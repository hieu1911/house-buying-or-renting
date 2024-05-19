using AutoMapper;
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
        private readonly IRealEstateRepository _realEstateRepository;

        private readonly IImageUrlService _imageUrlService;

        private readonly IMapper _mapper;

        public BoardingHouseService(IBoardingHouseRepository districtRepository, IRealEstateRepository realEstateRepository, IImageUrlService imageUrlService, IMapper mapper) : base(districtRepository)
        {
            _realEstateRepository = realEstateRepository;
            _imageUrlService = imageUrlService;
            _mapper = mapper;
        }

        public async override Task<int> InsertAsync(BoardingHouseCreateDto entityCreateDto)
        {

            var realEstate = _mapper.Map<RealEstate>(entityCreateDto.RealEstateCreateDto);
            var realEstateId = Guid.NewGuid();
            realEstate.CreatedDate = DateTime.Now;
            realEstate.CreatedName = "";
            realEstate.Id = realEstateId;

            var boardingHouse = await MapEntityCreateDtoToEntity(entityCreateDto);
            boardingHouse.Id = Guid.NewGuid();
            boardingHouse.RealEstateId = realEstateId;

            var imageUrlsCreateDto = entityCreateDto.RealEstateCreateDto.ImageUrlsCreateDto.Select(imageUrlCreateDto =>
            {
                imageUrlCreateDto.RealEstateId = realEstateId;
                return imageUrlCreateDto;
            }).ToList();

            await _realEstateRepository.InsertAsync(realEstate);
            await _imageUrlService.InsertMultiAsync(imageUrlsCreateDto);
            var result = await BaseRepository.InsertAsync(boardingHouse);

            return result;
        }

        public async override Task<BoardingHouse> MapEntityCreateDtoToEntity(BoardingHouseCreateDto entityCreateDto)
        {
            var boardingHouse = _mapper.Map<BoardingHouse>(entityCreateDto);
            return boardingHouse;
        }

        public async override Task<BoardingHouseDto> MapEntityToEntityDto(BoardingHouse entity)
        {
            var boardingHouseDto = _mapper.Map<BoardingHouseDto>(entity);
            return boardingHouseDto;
        }

        public async override Task<BoardingHouse> MapEntityUpdateDtoToEntity(Guid id, BoardingHouseUpdateDto entityUpdateDto)
        {
            var boardingHouse = _mapper.Map<BoardingHouse>(entityUpdateDto);
            boardingHouse.Id = id;
            return boardingHouse;
        }
    }
}
