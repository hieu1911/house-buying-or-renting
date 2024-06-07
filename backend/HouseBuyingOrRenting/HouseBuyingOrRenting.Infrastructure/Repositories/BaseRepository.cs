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

        public Task<int> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
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

        public Task<int> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
