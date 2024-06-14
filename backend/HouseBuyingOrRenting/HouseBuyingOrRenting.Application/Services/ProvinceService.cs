using AutoMapper;
using HouseBuyingOrRenting.Domain;
using HouseBuyingOrRenting.Infrastructure;
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

        private readonly IMapper _mapper;

        public ProvinceService(IProvinceRepository provinceRepository, IMapper mapper) : base(provinceRepository)
        {
            _provinceRepository = provinceRepository;
            _mapper = mapper;
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

        public override async Task<ProvinceDto> MapEntityToEntityDto(Province entity)
        {
            var province = _mapper.Map<ProvinceDto>(entity);
            return province;
        }

        public override Task<Province> MapEntityUpdateDtoToEntity(Guid id, ProvinceUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProvinceDto>> SearchByName(string value)
        {
            var provinces = await _provinceRepository.SearchByName(value);
            var districtsDto = provinces.Select(async disctrict => await MapEntityToEntityDto(disctrict))
                .Select(district => district.Result)
                .ToList();

            return districtsDto;
        }
    }
}
