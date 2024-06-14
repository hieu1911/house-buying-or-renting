using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public interface IDistrictRepository : IBaseRepository<District>
    {
        Task<List<District>> GetDistrictsByProvinceId(Guid provinceId);

        Task<List<Address>> GetDistrictsName();

        Task<List<District>> SearchByName(string value);
    }
}
