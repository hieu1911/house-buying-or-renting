using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class RealEstateService : BaseService<RealEstate, RealEstateDto, RealEstateCreateDto, RealEstateUpdateDto>, IRealEstateService
    {
        private readonly IRealEstateRepository _realEstateRepository;

        private readonly IMapper _mapper;

        public RealEstateService(IRealEstateRepository realEstateRepository, IMapper mapper) : base(realEstateRepository)
        {
            _realEstateRepository = realEstateRepository;
            _mapper = mapper;
        }

        public async Task<List<RealEstateDto>> FilterRealEstate(PostType type, string realEstateTypeStr, double minPrice, double maxPrice, double minArea, double maxArea)
        {
            var realEstateTypes = new List<RealEstateType>();
            foreach (var item in realEstateTypeStr)
            {
                var realEstateType = (RealEstateType) (item - '0');
                realEstateTypes.Add(realEstateType);
            }

            var realEstates = await _realEstateRepository.FilterRealEstate(type, realEstateTypes, minPrice, maxPrice, minArea, maxArea);

            var result = realEstates.Select(async realEstate => await MapEntityToEntityDto(realEstate))
                    .Select(realEstate => realEstate.Result).ToList();

            return result;
        }

        public Task<List<RealEstate>> GetAllRentRealEstate()
        {
            throw new NotImplementedException();
        }

        public async Task<List<RealEstateDto>> GetByDisctrictIds(List<Guid> ids)
        {
            var realEstates = await _realEstateRepository.GetByDisctrictIds(ids);

            var result = realEstates.Select(async realEstate => await MapEntityToEntityDto(realEstate))
                    .Select(realEstate => realEstate.Result).ToList();

            return result;
        }

        public async Task<List<RealEstateDto>> GetByOwner(Guid id)
        {

            var realEstates = await _realEstateRepository.GetByOwner(id);

            var result = realEstates.Select(async realEstate => await MapEntityToEntityDto(realEstate))
                    .Select(realEstate => realEstate.Result).ToList();

            return result;
        }

        public async Task<List<RealEstateDto>> GetByProvinceIds(List<Guid> ids)
        {
            var realEstates = await _realEstateRepository.GetByProvinceIds(ids);

            var result = realEstates.Select(async realEstate => await MapEntityToEntityDto(realEstate))
                    .Select(realEstate => realEstate.Result).ToList();

            return result;
        }

        public async Task<List<RealEstateDto>> GetList(Guid? provinceId, int pageSize, int pageNumber)
        {
            var realEstates = await _realEstateRepository.GetByProvinceId(provinceId, pageSize, pageNumber);

            var result = realEstates.Select(async realEstate => await MapEntityToEntityDto(realEstate))
                    .Select(realEstate => realEstate.Result).ToList();

            return result;
        }

        public async Task<List<RealEstateDto>> GetRealEstateForCarousel()
        {
            var realEstates = await _realEstateRepository.GetRealEstateForCarousel();

            var result = realEstates.Select(async realEstate => await MapEntityToEntityDto(realEstate))
                    .Select(realEstate => realEstate.Result).ToList();

            return result;
        }

        public async override Task<RealEstate> MapEntityCreateDtoToEntity(RealEstateCreateDto entityCreateDto)
        {
            var result = _mapper.Map<RealEstate>(entityCreateDto);
            return result;
        }

        public async override Task<RealEstateDto> MapEntityToEntityDto(RealEstate entity)
        {
            var result = _mapper.Map<RealEstateDto>(entity);
            return result;
        }

        public async override Task<RealEstate> MapEntityUpdateDtoToEntity(Guid id, RealEstateUpdateDto entityUpdateDto)
        {
            var result = _mapper.Map<RealEstate>(entityUpdateDto);
            result.Id = id;

            result.ImageUrls = entityUpdateDto.ImageUrlsCreateDto.Select(img => new ImageUrl() 
            {
                RealEstateId = id,
                Id = Guid.NewGuid(),
                Url = img.Url,
            }).ToList();


            return result;
        }

        public async Task<List<RealEstateDto>> SearchByTitle(string value)
        {
            var realEstates = await _realEstateRepository.SearchByTitle(value);

            var result = realEstates.Select(async realEstate => await MapEntityToEntityDto(realEstate))
                    .Select(realEstate => realEstate.Result).ToList();

            return result;
        }

        public bool CheckLatAndLong(double longitude, double latitude)
        {
            if (longitude < -180 || longitude > 180) return false;
            if (latitude < -90 || latitude > 90) return false;

            return true;
        }

        public async Task<int> ChangeStatus(Guid id, int status)
        {
            var result = await _realEstateRepository.ChangeStatus(id, status);
            return result;
        }
    }
}
