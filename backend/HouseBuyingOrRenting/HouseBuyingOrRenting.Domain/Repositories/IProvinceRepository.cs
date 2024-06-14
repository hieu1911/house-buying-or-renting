using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public interface IProvinceRepository : IBaseRepository<Province>
    {
        Task<List<Address>> GetProinvcesName();

        Task<List<Province>> SearchByName(string value);
    }
}
