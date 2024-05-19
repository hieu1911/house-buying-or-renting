using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class ApartmentService : BaseService<Apartment, ApartmentDto, ApartmentCreateDto, ApartmentUpdateDto>, IApartmentService
    {
        private IRealEstateRepository _realEstateRepository;

        private readonly IImageUrlService _imageUrlService;

        private readonly IMapper _mapper;

        public ApartmentService(IApartmentRepository districtRepository, IRealEstateRepository realEstateRepository, IImageUrlService imageUrlService, IMapper mapper) : base(districtRepository)
        {
            _realEstateRepository = realEstateRepository;
            _imageUrlService = imageUrlService;
            _mapper = mapper;
        }

        public async override Task<int> InsertAsync(ApartmentCreateDto entityCreateDto)
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

            await _realEstateRepository.InsertAsync(realEstate);
            await _imageUrlService.InsertMultiAsync(imageUrlsCreateDto);
            var result = await BaseRepository.InsertAsync(apartment);

            return result;
        }

        public async override Task<Apartment> MapEntityCreateDtoToEntity(ApartmentCreateDto entityCreateDto)
        {
            var apartment = _mapper.Map<Apartment>(entityCreateDto);
            return apartment;
        }

        public async override Task<ApartmentDto> MapEntityToEntityDto(Apartment entity)
        {
            var apartmentDto = _mapper.Map<ApartmentDto>(entity);
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
