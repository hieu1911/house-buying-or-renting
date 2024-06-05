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
                .SingleOrDefault<Land>(l => l.RealEstateId == realEstateId);

            return land;
        }
    }
}
