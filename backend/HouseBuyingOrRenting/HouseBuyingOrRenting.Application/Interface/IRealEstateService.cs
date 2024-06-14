using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public interface IRealEstateService : IBaseService<RealEstate, RealEstateDto, RealEstateCreateDto, RealEstateUpdateDto>
    {
        Task<List<RealEstate>> GetAllRentRealEstate();

        Task<List<RealEstateDto>> GetRealEstateForCarousel();

        Task<List<RealEstateDto>> GetList(Guid? provinceId, int pageSize, int pageNumber);

        Task<List<RealEstateDto>> GetByOwner(Guid id);

        Task<List<RealEstateDto>> SearchByTitle(string value);

        Task<List<RealEstateDto>> GetByProvinceIds(List<Guid> ids);

        Task<List<RealEstateDto>> GetByDisctrictIds(List<Guid> ids);

        Task<List<RealEstateDto>> FilterRealEstate(PostType type, string realEstateTypeStr
            , double minPrice, double maxPrice, double minArea, double maxArea);

    }
}
