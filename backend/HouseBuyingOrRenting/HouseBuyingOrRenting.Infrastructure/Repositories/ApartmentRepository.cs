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
                .ThenInclude(r => r.District)
                .SingleOrDefault<Apartment>(a => a.RealEstateId == realEstateId);

            return apartment;
        }

        public async override Task<int> UpdateAsync(Apartment entity)
        {
            var apartment = await _db.Apartments.FindAsync(entity.Id);
            if (apartment != null)
            {
                apartment.NumberOfBedRoom = entity.NumberOfBedRoom;
                apartment.NumberOfToilet = entity.NumberOfToilet;
                apartment.Floor = entity.Floor;
                apartment.Funiture = entity.Funiture;
                apartment.LegalDocument = entity.LegalDocument;
            }

            return 0;
        }
    }
}
