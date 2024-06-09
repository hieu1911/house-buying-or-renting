using AutoMapper;
using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class PostSaveService : BaseService<PostSave, PostSave, PostSaveCreateDto, PostSave>, IPostSaveService
    {
        private readonly IMapper _mapper;

        private readonly IPostSaveRepository _postSaveRepository;

        public PostSaveService(IPostSaveRepository postSaveRepository, IMapper mapper) : base(postSaveRepository)
        {
            _mapper = mapper;
            _postSaveRepository = postSaveRepository;
        }

        public async Task<PostSave> GetBuyUserIdAndRealEstateId(Guid userId, Guid realEstateId)
        {
            var result = await _postSaveRepository.GetBuyUserIdAndRealEstateId(userId, realEstateId);
            return result;
        }

        public async Task<List<Guid>> GetRealEstateIdsByUser(Guid userId)
        {
            var result = await _postSaveRepository.GetRealEstateIdsByUser(userId);
            return result;
        }

        public async override Task<PostSave> MapEntityCreateDtoToEntity(PostSaveCreateDto entityCreateDto)
        {
            return _mapper.Map<PostSave>(entityCreateDto);
        }

        public async override Task<PostSave> MapEntityToEntityDto(PostSave entity)
        {
            return entity;
        }

        public async override Task<PostSave> MapEntityUpdateDtoToEntity(Guid id, PostSave entityUpdateDto)
        {
            return entityUpdateDto;
        }
    }
}
