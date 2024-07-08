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
                .ThenInclude(r => r.District)
                .SingleOrDefault<BoardingHouse>(b => b.RealEstateId == realEstateId);

            return boardingHouse;
        }

        public async override Task<int> UpdateAsync(BoardingHouse entity)
        {
            var boardingHouse = await _db.BoardingHouses.FindAsync(entity.Id);
            if (boardingHouse == null)
            {
                boardingHouse.Funiture = entity.Funiture;
                boardingHouse.SeftContained = entity.SeftContained;

                await _db.SaveChangesAsync();
                return 1;
            }

            return 0;
        }
    }
}
