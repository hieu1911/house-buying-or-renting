using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public interface IHouseService : IBaseService<House, HouseDto, HouseCreateDto, HouseUpdateDto>
    {
        Task<HouseDto> GetByRealEstateId(Guid realEstateId);
    }
}
