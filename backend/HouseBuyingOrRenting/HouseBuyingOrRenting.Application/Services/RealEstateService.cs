using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class RealEstateService : BaseService<RealEstate, RealEstateDto, RealEstateCreateDto, RealEstateUpdateDto>, IRealEstateService
    {
        public RealEstateService(IRealEstateRepository realEstateRepository) : base(realEstateRepository)
        {
        }

        public override Task<RealEstate> MapEntityCreateDtoToEntity(RealEstateCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<RealEstateDto> MapEntityToEntityDto(RealEstate entity)
        {
            throw new NotImplementedException();
        }

        public override Task<RealEstate> MapEntityUpdateDtoToEntity(Guid id, RealEstateUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
