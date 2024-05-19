using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class RealEstateRepository : BaseRepository<RealEstate>, IRealEstateRepository
    {
        public DbSet<RealEstate> _dbSet { get; set; }

        public RealEstateRepository(MyDbContext db) : base(db, db.RealEstates)
        {
            _dbSet = db.RealEstates;
        }

        public async Task<List<RealEstate>> GetRealEstateForCarousel()
        {
            var realEstateRent = _dbSet
               .Where(e => e.Type == PostType.Renting)
               .Take(8)
               .ToList();

            var realEstateBuy = _dbSet
                .Where(e => e.Type == PostType.Buying)
                .Take(8)
                .ToList();

            var result = realEstateRent.Concat(realEstateBuy).ToList();

            return result;
        }
    }
}
