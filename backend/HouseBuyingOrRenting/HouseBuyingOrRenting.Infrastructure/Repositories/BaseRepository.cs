using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private MyDbContext _dbContext;

        private DbSet<TEntity> _dbSet;

        public BaseRepository(MyDbContext dbContext, DbSet<TEntity> dbSet)
        {
            _dbContext = dbContext;
            _dbSet = dbSet;
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var entity = _dbSet.SingleOrDefault(x => x.Id == id);
            if (entity == null) throw new NotFoundException();
            
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();

            return 1;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();

            return entities;
        }

        public virtual async Task<TEntity?> GetAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            return entity;
        }

        public virtual async Task<List<TEntity>> GetByIdsAsync(List<Guid> ids)
        {
            var result = _dbSet.Where(e => ids.Contains(e.Id)).ToList();
            return result;
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return 1;
        }

        public async Task<int> InsertMultiAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();

            return entities.Count;
        }

        public virtual Task<int> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
