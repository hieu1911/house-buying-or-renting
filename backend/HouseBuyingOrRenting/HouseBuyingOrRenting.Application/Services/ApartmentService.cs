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
        public ApartmentService(IApartmentRepository districtRepository) : base(districtRepository)
        {
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
