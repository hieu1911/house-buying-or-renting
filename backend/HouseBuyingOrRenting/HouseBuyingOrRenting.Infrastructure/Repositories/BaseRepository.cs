using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();

            return entities;
        }

        public async Task<TEntity?> GetAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            return entity;
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            var result = await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return 1;
        }

        public Task<int> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
