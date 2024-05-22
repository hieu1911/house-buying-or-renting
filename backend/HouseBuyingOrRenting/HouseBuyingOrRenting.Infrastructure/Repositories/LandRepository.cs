using HouseBuyingOrRenting.Domain;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class LandRepository : BaseRepository<Land>, ILandRepository
    {
        public LandRepository(MyDbContext db) : base(db, db.Lands)
        {
        }
    }
}
