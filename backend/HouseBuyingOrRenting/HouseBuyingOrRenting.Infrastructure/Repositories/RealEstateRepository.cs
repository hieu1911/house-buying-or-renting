using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class RealEstateRepository : BaseRepository<RealEstate>, IRealEstateRepository
    {
        private MyDbContext _db;

        private DbSet<RealEstate> _dbSet { get; set; }

        public RealEstateRepository(MyDbContext db) : base(db, db.RealEstates)
        {
            _db = db;
            _dbSet = db.RealEstates;
        }

        public override async Task<RealEstate?> GetAsync(Guid id)
        {
            var realEstate = await _dbSet.Include(r => r.ImageUrls).FirstOrDefaultAsync(r => r.Id == id);
            return realEstate;
        }

        public override async Task<List<RealEstate>> GetAllAsync()
        {
            var realEstates = _dbSet
                .Include(e => e.ImageUrls)
                .Include(e => e.District)
                .ThenInclude(d => d.Province)
                .OrderByDescending(e => e.CreatedDate)
                .ToList();

            return realEstates;
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

            return realEstates;
        }

        public async Task<List<RealEstate>> GetByOwner(Guid id)
        {
            var realEstates = _dbSet
                .Include(e => e.ImageUrls)
                .Include(e => e.District)
                .ThenInclude(d => d.Province)
                .Where(r => r.OwnerId == id)
                .OrderByDescending(e => e.CreatedDate)
                .ToList();

            return realEstates;
        }

        public override async Task<List<RealEstate>> GetByIdsAsync(List<Guid> ids)
        {
            var realEstates = _dbSet
               .Include(e => e.ImageUrls)
               .Include(e => e.District)
               .ThenInclude(d => d.Province)
               .Where(r => ids.Contains(r.Id))
               .OrderByDescending(e => e.CreatedDate)
               .ToList();

            return realEstates;
        }

        public async Task<List<RealEstate>> SearchByTitle(string value)
        {
            var realEstates = _dbSet
             .Include(e => e.ImageUrls)
             .Include(e => e.District)
             .ThenInclude(d => d.Province)
             .Where(r => r.Title.Contains(value))
             .OrderByDescending(e => e.CreatedDate)
             .ToList();

            return realEstates;
        }

        public async Task<List<RealEstate>> GetByProvinceIds(List<Guid> ids)
        {
            var realEstates = _dbSet
             .Include(e => e.ImageUrls)
             .Include(e => e.District)
             .ThenInclude(d => d.Province)
             .Where(r => ids.Contains(r.District.Province.Id))
             .OrderByDescending(e => e.CreatedDate)
             .ToList();

            return realEstates;
        }

        public async Task<List<RealEstate>> GetByDisctrictIds(List<Guid> ids)
        {
            var realEstates = _dbSet
              .Include(e => e.ImageUrls)
              .Include(e => e.District)
              .ThenInclude(d => d.Province)
              .Where(r => ids.Contains(r.District.Id))
              .OrderByDescending(e => e.CreatedDate)
              .ToList();

            return realEstates;

        }

        public async Task<List<RealEstate>> FilterRealEstate(PostType type, List<RealEstateType> realEstateTypes, double minPrice, double maxPrice, double minArea, double maxArea)
        {
            var realEstates = _dbSet
              .Include(e => e.ImageUrls)
              .Include(e => e.District)
              .ThenInclude(d => d.Province)
              .Where(r => r.Type == type)
              .Where(r => realEstateTypes.Contains(r.RealEstateType))
              .Where(r => r.Price >= minPrice && r.Price <= maxPrice)
              .Where(r => r.Area >= minArea && r.Area <= maxArea)
              .OrderByDescending(e => e.CreatedDate)
              .ToList();

            return realEstates;
        }

        public async Task<int> ChangeStatus(Guid id, int status)
        {
            var realEstate = await _dbSet.FindAsync(id);
            if (realEstate != null)
            {
                realEstate.IsAccepted = status;
                await _db.SaveChangesAsync();

                return 1;
            }

            return 0;
        }

        public async override Task<int> UpdateAsync(RealEstate entity)
        {
            var realEstate = await _dbSet.FindAsync(entity.Id);
            if (realEstate != null)
            {
                realEstate.IsPersonal = entity.IsPersonal;
                realEstate.DistrictId = entity.DistrictId;
                realEstate.Address = entity.Address;
                realEstate.Latitude = entity.Latitude;
                realEstate.Longtitude = entity.Longtitude;
                realEstate.Area = entity.Area;
                realEstate.Title = entity.Title;
                realEstate.Name = entity.Name;
                realEstate.Description = entity.Description;
                realEstate.Price = entity.Price;
                realEstate.Feature = entity.Feature;
                realEstate.Type = entity.Type;
                realEstate.RealEstateType = entity.RealEstateType;
                realEstate.IsAccepted = 0;

                _db.ImageUrls.Where(img => img.RealEstateId == entity.Id).ExecuteDelete();

                await _db.ImageUrls.AddRangeAsync(entity.ImageUrls);

                await _db.SaveChangesAsync();
                return 1;
            }

            return 0;
        }

        public async override Task<int> DeleteAsync(Guid id)
        {
            var realEstate = await _db.RealEstates.FindAsync(id);
            if (realEstate != null)
            {
                realEstate.IsDeleted = true;

                await _db.SaveChangesAsync();
                return 1;
            }

            return 0;
        }
    }
}
