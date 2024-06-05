using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class ApartmentRepository : BaseRepository<Apartment>, IApartmentRepository
    {
        private readonly MyDbContext _db;

        public ApartmentRepository(MyDbContext db) : base(db, db.Apartments)
        {
            _db = db;
        }

        public async Task<Apartment> GetByRealEstateId(Guid realEstateId)
        {
            var apartment = _db.Apartments
                .Include(a => a.RealEstate)
                .SingleOrDefault<Apartment>(a => a.RealEstateId == realEstateId);

            return apartment;
        }
    }
}
