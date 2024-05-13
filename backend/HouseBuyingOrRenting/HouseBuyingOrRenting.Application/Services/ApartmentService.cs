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
        private readonly IImageUrlService _imageUrlService;

        public ApartmentService(IApartmentRepository districtRepository, IImageUrlService imageUrlService) : base(districtRepository)
        {
            _imageUrlService = imageUrlService;
        }

        public async override Task<int> InsertAsync(ApartmentCreateDto entityCreateDto)
        {

            var apartment = await MapEntityCreateDtoToEntity(entityCreateDto);
            apartment.CreatedDate = DateTime.Now;
            apartment.CreatedName = "";
            apartment.Id = Guid.NewGuid();

            var imageUrlsCreateDto = entityCreateDto.ImageUrlsCreateDto.Select(imageUrlCreateDto =>
            {
                imageUrlCreateDto.RealEstateId = apartment.Id;
                return imageUrlCreateDto;
            }).ToList();

            var result = await BaseRepository.InsertAsync(apartment);
            await _imageUrlService.InsertMultiAsync(imageUrlsCreateDto);

            return result;
        }

        public override Task<Apartment> MapEntityCreateDtoToEntity(ApartmentCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<ApartmentDto> MapEntityToEntityDto(Apartment entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Apartment> MapEntityUpdateDtoToEntity(Guid id, ApartmentUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
