using HouseBuyingOrRenting.Domain;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class ApartmentRepository : BaseRepository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(MyDbContext db) : base(db, db.Apartments)
        {
        }
    }
}
