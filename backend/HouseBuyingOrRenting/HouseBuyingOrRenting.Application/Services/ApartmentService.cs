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
    public class ApartmentService : BaseService<Apartment, ApartmentDto, ApartmentCreateDto, ApartmentUpdateDto>, IApartmentService
    {
        private readonly IRealEstateRepository _realEstateRepository;

        private readonly IApartmentRepository _apartmentRepository;

        private readonly IImageUrlService _imageUrlService;

        private readonly IMapper _mapper;

        private readonly MyDbContext _db;

        public ApartmentService(
            IApartmentRepository apartmentRepository, 
            IRealEstateRepository realEstateRepository, 
            IImageUrlService imageUrlService, 
            IMapper mapper,
            MyDbContext db
            ) : base(apartmentRepository)
        {
            _realEstateRepository = realEstateRepository;
            _apartmentRepository = apartmentRepository;
            _imageUrlService = imageUrlService;
            _mapper = mapper;
            _db = db;
        }

        public async Task<ApartmentDto> GetByRealEstateId(Guid realEstateId)
        {
            var apartment = await _apartmentRepository.GetByRealEstateId(realEstateId);
            var apartmentDto = await MapEntityToEntityDto(apartment);

            return apartmentDto;
        }


        public async override Task<Guid> InsertAsync(ApartmentCreateDto entityCreateDto)
        {

            var realEstate = _mapper.Map<RealEstate>(entityCreateDto.RealEstateCreateDto);
            var realEstateId = Guid.NewGuid();
            realEstate.CreatedDate = DateTime.Now;
            realEstate.CreatedName = "";
            realEstate.Id = realEstateId;

            var apartment = await MapEntityCreateDtoToEntity(entityCreateDto);
            apartment.Id = Guid.NewGuid();
            apartment.RealEstateId = realEstateId;

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
                    var result = await BaseRepository.InsertAsync(apartment);
                    transaction.Commit();
                    return realEstate.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async override Task<Apartment> MapEntityCreateDtoToEntity(ApartmentCreateDto entityCreateDto)
        {
            var apartment = _mapper.Map<Apartment>(entityCreateDto);
            return apartment;
        }

        public async override Task<ApartmentDto> MapEntityToEntityDto(Apartment entity)
        {
            var apartmentDto = _mapper.Map<ApartmentDto>(entity);
            apartmentDto.RealEstateDto = _mapper.Map<RealEstateDto>(entity.RealEstate);
            return apartmentDto;
        }

        public async override Task<Apartment> MapEntityUpdateDtoToEntity(Guid id, ApartmentUpdateDto entityUpdateDto)
        {
            var apartment = _mapper.Map<Apartment>(entityUpdateDto);
            apartment.Id = id;
            return apartment;
        }
    }
}
