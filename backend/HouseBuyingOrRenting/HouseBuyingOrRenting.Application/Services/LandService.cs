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
    public class LandService : BaseService<Land, LandDto, LandCreateDto, LandUpdateDto>, ILandService
    {
        private readonly IRealEstateRepository _realEstateRepository;

        private readonly IImageUrlService _imageUrlService;

        private readonly IMapper _mapper;

        private readonly MyDbContext _db;

        public LandService(
            ILandRepository districtRepository,
            IRealEstateRepository realEstateRepository,
            IImageUrlService imageUrlService,
            IMapper mapper,
            MyDbContext db
            ) : base(districtRepository)
        {
            _realEstateRepository = realEstateRepository;
            _imageUrlService = imageUrlService;
            _mapper = mapper;
            _db = db;
        }

        public async override Task<int> InsertAsync(LandCreateDto entityCreateDto)
        {

            var realEstate = _mapper.Map<RealEstate>(entityCreateDto.RealEstateCreateDto);
            var realEstateId = Guid.NewGuid();
            realEstate.CreatedDate = DateTime.Now;
            realEstate.CreatedName = "";
            realEstate.Id = realEstateId;

            var land = await MapEntityCreateDtoToEntity(entityCreateDto);
            land.Id = Guid.NewGuid();
            land.RealEstateId = realEstateId;

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
                    var result = await BaseRepository.InsertAsync(land);
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

        public override async Task<Land> MapEntityCreateDtoToEntity(LandCreateDto entityCreateDto)
        {
            var land = _mapper.Map<Land>(entityCreateDto);
            return land;
        }

        public override async Task<LandDto> MapEntityToEntityDto(Land entity)
        {
            var landDto = _mapper.Map<LandDto>(entity);
            return landDto;
        }

        public override async Task<Land> MapEntityUpdateDtoToEntity(Guid id, LandUpdateDto entityUpdateDto)
        {
            var land = _mapper.Map<Land>(entityUpdateDto);
            land.Id = id;
            return land;
        }
    }
}
