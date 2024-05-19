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

        public Task<List<RealEstate>> GetAllRentRealEstate()
        {
            throw new NotImplementedException();
        }

        public async Task<List<RealEstateDto>> GetRealEstateForCarousel()
        {
            var realEstates = await _realEstateRepository.GetRealEstateForCarousel();

            var result = realEstates.Select(async realEstate => await MapEntityToEntityDto(realEstate))
                    .Select(realEstate => realEstate.Result).ToList();

            return result;
        }

        public override Task<RealEstate> MapEntityCreateDtoToEntity(RealEstateCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public async override Task<RealEstateDto> MapEntityToEntityDto(RealEstate entity)
        {
            var result = _mapper.Map<RealEstateDto>(entity);
            return result;
        }

        public override Task<RealEstate> MapEntityUpdateDtoToEntity(Guid id, RealEstateUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
