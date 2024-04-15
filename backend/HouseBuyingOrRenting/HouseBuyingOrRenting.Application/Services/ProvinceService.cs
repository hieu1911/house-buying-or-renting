using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class ProvinceService : BaseService<Province, ProvinceDto, ProvinceCreateDto, ProvinceUpdateDto>, IProvinceService
    {
        public ProvinceService(IProvinceRepository provinceRepository) : base(provinceRepository)
        {
        }

        public override Task<Province> MapEntityCreateDtoToEntity(ProvinceCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<ProvinceDto> MapEntityToEntityDto(Province entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Province> MapEntityUpdateDtoToEntity(Guid id, ProvinceUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
