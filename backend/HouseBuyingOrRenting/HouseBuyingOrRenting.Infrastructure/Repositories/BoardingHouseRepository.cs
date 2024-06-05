using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class BoardingHouseRepository : BaseRepository<BoardingHouse>, IBoardingHouseRepository
    {
        private MyDbContext _db;

        public BoardingHouseRepository(MyDbContext db) : base(db, db.BoardingHouses)
        {
            _db = db;
        }

        public async Task<BoardingHouse> GetByRealEstateId(Guid realEstateId)
        {
            var boardingHouse = _db.BoardingHouses
                .Include(b => b.RealEstate)
                .SingleOrDefault<BoardingHouse>(b => b.RealEstateId == realEstateId);

            return boardingHouse;
        }
    }
}
