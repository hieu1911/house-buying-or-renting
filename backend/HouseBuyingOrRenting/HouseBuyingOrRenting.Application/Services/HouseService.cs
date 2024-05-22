using AutoMapper;
using HouseBuyingOrRenting.Domain;
using HouseBuyingOrRenting.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class HouseService : BaseService<House, HouseDto, HouseCreateDto, HouseUpdateDto>, IHouseService
    {
        private readonly IRealEstateRepository _realEstateRepository;

        private readonly IImageUrlService _imageUrlService;

        private readonly IMapper _mapper;

        private readonly MyDbContext _db;

        public HouseService(
            IHouseRepository houseRepository, 
            IRealEstateRepository realEstateRepository, 
            IImageUrlService imageUrlService, 
            IMapper mapper,
            MyDbContext db
            ) : base(houseRepository)
        {
            _realEstateRepository = realEstateRepository;
            _imageUrlService = imageUrlService;
            _mapper = mapper;
            _db = db;
        }

        public async override Task<int> InsertAsync(HouseCreateDto entityCreateDto)
        {
            
            var realEstate = _mapper.Map<RealEstate>(entityCreateDto.RealEstateCreateDto);
            var realEstateId = Guid.NewGuid();
            realEstate.CreatedDate = DateTime.Now;
            realEstate.CreatedName = "";
            realEstate.Id = realEstateId;

            var house = await MapEntityCreateDtoToEntity(entityCreateDto);
            house.Id = Guid.NewGuid();
            house.RealEstateId = realEstateId;

            var imageUrlsCreateDto = entityCreateDto.RealEstateCreateDto.ImageUrlsCreateDto.Select(imageUrlCreateDto =>
            {
                imageUrlCreateDto.RealEstateId = realEstateId;
                return imageUrlCreateDto;
            }).ToList();

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    await _realEstateRepository.InsertAsync(realEstate);
                    await _imageUrlService.InsertMultiAsync(imageUrlsCreateDto);
                    var result = await BaseRepository.InsertAsync(house);
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public override async Task<House> MapEntityCreateDtoToEntity(HouseCreateDto entityCreateDto)
        {
            var house = _mapper.Map<House>(entityCreateDto);
            return house;
        }

        public override async Task<HouseDto> MapEntityToEntityDto(House entity)
        {
            var houseDto = _mapper.Map<HouseDto>(entity);
            return houseDto;
        }

        public override async Task<House> MapEntityUpdateDtoToEntity(Guid id, HouseUpdateDto entityUpdateDto)
        {
            var house = _mapper.Map<House>(entityUpdateDto);
            house.Id = id;
            return house;
        }
    }
}
