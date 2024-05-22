using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class RealEstateRepository : BaseRepository<RealEstate>, IRealEstateRepository
    {
        private DbSet<RealEstate> _dbSet { get; set; }

        public RealEstateRepository(MyDbContext db) : base(db, db.RealEstates)
        {
            _dbSet = db.RealEstates;
        }

        public async Task<List<RealEstate>> GetRealEstateForCarousel()
        {
            var realEstateRent = _dbSet
               .Where(e => e.Type == PostType.Renting)
               .Include(e => e.ImageUrls)
               .Include(e => e.District)
               .ThenInclude(d => d.Province)
               .OrderByDescending(e => e.CreatedDate)
               .Take(8)
               .ToList();

            var realEstateBuy = _dbSet
                .Where(e => e.Type == PostType.Buying)
                .Include(e => e.ImageUrls)
                .Include(e => e.District)
                .ThenInclude(d => d.Province)
                .OrderByDescending(e => e.CreatedDate)
                .Take(8)
                .ToList();

            var result = realEstateRent.Concat(realEstateBuy).ToList();

            return result;
        }
    }
}
