using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public interface IDistrictService : IBaseService<District, DistrictDto, DistrictCreateDto, DistrictUpdateDto>
    {
        Task<List<DistrictDto>> GetDistrictsByProvinceId(Guid provinceId);

        Task<List<Address>> GetDistrictsName();
    }
}
