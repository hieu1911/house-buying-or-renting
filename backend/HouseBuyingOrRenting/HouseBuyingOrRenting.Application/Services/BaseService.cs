using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto>
        where TEntity : BaseEntity where TEntityDto : class where TEntityCreateDto : class where TEntityUpdateDto : class
    {
        protected IBaseRepository<TEntity> BaseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            BaseRepository = baseRepository;
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await BaseRepository.GetAllAsync();

            return entities;
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await BaseRepository.GetAsync(id);

            if (entity is null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public virtual async Task<int> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            var entity = await MapEntityCreateDtoToEntity(entityCreateDto);
            entity.CreatedDate = DateTime.Now;
            entity.CreatedName = "";
            entity.Id = Guid.NewGuid();

            var result = await BaseRepository.InsertAsync(entity);
            return result;
        }

        public async Task<int> InsertMultiAsync(List<TEntityCreateDto> entitiesCreateDto)
        {
            var entities = entitiesCreateDto.Select(async entityCreateDto => await MapEntityCreateDtoToEntity(entityCreateDto))
                .Select(entity => entity.Result).ToList();

            var result = await BaseRepository.InsertMultiAsync(entities);
            return result;
        }

        public Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }

        public abstract Task<TEntity> MapEntityCreateDtoToEntity(TEntityCreateDto entityCreateDto);

        public abstract Task<TEntity> MapEntityUpdateDtoToEntity(Guid id, TEntityUpdateDto entityUpdateDto);

        public abstract Task<TEntityDto> MapEntityToEntityDto(TEntity entity);
    }
}
