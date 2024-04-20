using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class BoardingHouseService : BaseService<BoardingHouse, BoardingHouseDto, BoardingHouseCreateDto, BoardingHouseUpdateDto>, IBoardingHouseService
    {
        public BoardingHouseService(IBoardingHouseRepository districtRepository) : base(districtRepository)
        {
        }

        public override Task<BoardingHouse> MapEntityCreateDtoToEntity(BoardingHouseCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<BoardingHouseDto> MapEntityToEntityDto(BoardingHouse entity)
        {
            throw new NotImplementedException();
        }

        public override Task<BoardingHouse> MapEntityUpdateDtoToEntity(Guid id, BoardingHouseUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
