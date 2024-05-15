using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
    {
        private readonly DbSet<Province> _dbSet;

        public ProvinceRepository(MyDbContext db) : base(db, db.Provinces)
        {
            _dbSet = db.Provinces;
        }

        public override async Task<List<Province>> GetAllAsync()
        {
            var result = await _dbSet.OrderBy(p => p.Name).ToListAsync();

            return result;
        }
    }
}
