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
        private readonly IProvinceRepository _provinceRepository;

        public ProvinceService(IProvinceRepository provinceRepository) : base(provinceRepository)
        {
            _provinceRepository = provinceRepository;
        }

        public async Task<List<Address>> GetProinvcesName()
        {
            var result = await _provinceRepository.GetProinvcesName();
            return result;
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
