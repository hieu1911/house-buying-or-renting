using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class DistrictService : BaseService<District, DistrictDto, DistrictCreateDto, DistrictUpdateDto>, IDistrictService
    {
        public DistrictService(IDistrictRepository districtRepository) : base(districtRepository)
        {
        }

        public override Task<District> MapEntityCreateDtoToEntity(DistrictCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<DistrictDto> MapEntityToEntityDto(District entity)
        {
            throw new NotImplementedException();
        }

        public override Task<District> MapEntityUpdateDtoToEntity(Guid id, DistrictUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
