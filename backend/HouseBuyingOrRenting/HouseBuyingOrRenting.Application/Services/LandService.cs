using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class LandService : BaseService<Land, LandDto, LandCreateDto, LandUpdateDto>, ILandService
    {
        public LandService(ILandRepository districtRepository) : base(districtRepository)
        {
        }

        public override Task<Land> MapEntityCreateDtoToEntity(LandCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<LandDto> MapEntityToEntityDto(Land entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Land> MapEntityUpdateDtoToEntity(Guid id, LandUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
