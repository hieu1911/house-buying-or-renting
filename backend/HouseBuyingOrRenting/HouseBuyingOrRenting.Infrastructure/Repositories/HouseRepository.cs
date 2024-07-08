using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class HouseRepository : BaseRepository<House>, IHouseRepository
    {
        private readonly MyDbContext _db;

        public HouseRepository(MyDbContext db) : base(db, db.Houses)
        {
            _db = db;
        }

        public async override Task<List<House>> GetAllAsync()
        {
            var result = _db.Houses.Include(house => house.RealEstate).ToList();

            return result;
        }

        public async Task<House> GetByRealEstateId(Guid realEstateId)
        {
            var house = _db.Houses
                .Include(h => h.RealEstate)
                .ThenInclude(r => r.District)
                .SingleOrDefault<House>(h => h.RealEstateId == realEstateId);

            return house;
        }

        public async override Task<int> UpdateAsync(House entity)
        {
            var house = await _db.Houses.FindAsync(entity.Id);
            if (house != null)
            {
                house.NumberOfBedRoom = entity.NumberOfBedRoom;
                house.NumberOfToilet = entity.NumberOfToilet;
                house.NumberOfFloor = entity.NumberOfFloor;
                house.Funiture = entity.Funiture;
                house.RedBook = entity.RedBook;

                await _db.SaveChangesAsync();
                return 1;
            }

            return 0;
        }
    }
}
