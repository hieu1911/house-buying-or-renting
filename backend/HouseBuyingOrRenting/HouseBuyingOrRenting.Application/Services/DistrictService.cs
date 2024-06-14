using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class DistrictService : BaseService<District, DistrictDto, DistrictCreateDto, DistrictUpdateDto>, IDistrictService
    {
        private readonly IDistrictRepository _disctrictRepository;
        private readonly IMapper _mapper;

        public DistrictService(IDistrictRepository districtRepository, IMapper mapper) : base(districtRepository)
        {
            _disctrictRepository = districtRepository;
            _mapper = mapper;
        }

        public async Task<List<DistrictDto>> GetDistrictsByProvinceId(Guid provinceId)
        {
            var districts = await _disctrictRepository.GetDistrictsByProvinceId(provinceId);
            var districtsDto = districts.Select(async disctrict => await MapEntityToEntityDto(disctrict))
                .Select(district => district.Result)
                .ToList();

            return districtsDto;
        }

        public async Task<List<Address>> GetDistrictsName()
        {
            var result = await _disctrictRepository.GetDistrictsName();
            return result;
        }

        public override async Task<District> MapEntityCreateDtoToEntity(DistrictCreateDto entityCreateDto)
        {
            var district = _mapper.Map<District>(entityCreateDto);
            return district;
        }

        public override async Task<DistrictDto> MapEntityToEntityDto(District entity)
        {
            var district = _mapper.Map<DistrictDto>(entity);
            return district;
        }

        public override async Task<District> MapEntityUpdateDtoToEntity(Guid id, DistrictUpdateDto entityUpdateDto)
        {
            var district = _mapper.Map<District>(entityUpdateDto);
            district.Id = id;
            return district;
        }

        public async Task<List<DistrictDto>> SearchByName(string value)
        {
            var districts = await _disctrictRepository.SearchByName(value);
            var districtsDto = districts.Select(async disctrict => await MapEntityToEntityDto(disctrict))
                .Select(district => district.Result)
                .ToList();

            return districtsDto;
        }
    }
}
