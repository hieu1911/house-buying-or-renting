using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public interface IProvinceService : IBaseService<Province, ProvinceDto, ProvinceCreateDto, ProvinceUpdateDto>
    {
        Task<List<Address>> GetProinvcesName();

        Task<List<ProvinceDto>> SearchByName(string value);
    }
}
