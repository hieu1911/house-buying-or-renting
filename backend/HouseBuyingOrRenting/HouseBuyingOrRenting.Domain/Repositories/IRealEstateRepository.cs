using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public interface IRealEstateRepository : IBaseRepository<RealEstate>
    {
        Task<List<RealEstate>> GetRealEstateForCarousel();

        Task<List<RealEstate>> GetByProvinceId(Guid? provinceId, int pageSize, int pageNumber);

        Task<List<RealEstate>> GetByOwner(Guid id);
    }
}
