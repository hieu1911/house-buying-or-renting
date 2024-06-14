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

        Task<List<RealEstate>> SearchByTitle(string value);

        Task<List<RealEstate>> GetByProvinceIds(List<Guid> ids);

        Task<List<RealEstate>> GetByDisctrictIds(List<Guid> ids);

        Task<List<RealEstate>> FilterRealEstate(PostType type, List<RealEstateType> realEstateTypes
            , double minPrice, double maxPrice, double minArea, double maxArea);
    }
}
