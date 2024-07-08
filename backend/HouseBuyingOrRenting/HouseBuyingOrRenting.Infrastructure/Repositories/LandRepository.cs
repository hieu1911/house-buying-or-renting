using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class LandRepository : BaseRepository<Land>, ILandRepository
    {
        private readonly MyDbContext _db;

        public LandRepository(MyDbContext db) : base(db, db.Lands)
        {
            _db = db;
        }

        public async Task<Land> GetByRealEstateId(Guid realEstateId)
        {
            var land = _db.Lands
                .Include(l => l.RealEstate)
                .ThenInclude(r => r.District)
                .SingleOrDefault<Land>(l => l.RealEstateId == realEstateId);

            return land;
        }

        public async override Task<int> UpdateAsync(Land entity)
        {
            var land = await _db.Lands.FindAsync(entity.Id);
            if (land == null)
            {
                land.LandType = entity.LandType;
                land.LegalDocument = entity.LegalDocument;

                await _db.SaveChangesAsync();
                return 1;
            }

            return 0;
        }
    }
}
