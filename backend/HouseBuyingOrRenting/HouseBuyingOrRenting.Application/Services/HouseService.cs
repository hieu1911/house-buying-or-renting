using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class HouseService : BaseService<House, HouseDto, HouseCreateDto, HouseUpdateDto>, IHouseService
    {
        public HouseService(IHouseRepository realEstateRepository) : base(realEstateRepository)
        {
        }

        public override Task<House> MapEntityCreateDtoToEntity(HouseCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<HouseDto> MapEntityToEntityDto(House entity)
        {
            throw new NotImplementedException();
        }

        public override Task<House> MapEntityUpdateDtoToEntity(Guid id, HouseUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
