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

        public override async Task<RealEstate?> GetAsync(Guid id)
        {
            var realEstate = await _dbSet.Include(r => r.ImageUrls).FirstOrDefaultAsync(r => r.Id == id);
            return realEstate;
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

        public async Task<List<RealEstate>> GetByProvinceId(Guid? provinceId, int pageSize, int pageNumber)
        {
            var realEstates = _dbSet
                .Include(e => e.ImageUrls)
                .Include(e => e.District)
                .ThenInclude(d => d.Province)
                .Where(e => provinceId == null || e.District.ProvinceId == provinceId)
                .OrderByDescending(e => e.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return realEstates.ToList();
        }
    }
}
