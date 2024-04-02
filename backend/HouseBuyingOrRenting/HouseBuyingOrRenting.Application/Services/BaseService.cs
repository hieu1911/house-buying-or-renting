using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application.Services
{
    public class BaseService<TEntity, TEntityCreateDto, TEntityUpdateDto> : IBaseService<TEntity, TEntityCreateDto, TEntityUpdateDto>
        where TEntity : BaseEntity where TEntityCreateDto : class where TEntityUpdateDto : class
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

        public Task<int> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
