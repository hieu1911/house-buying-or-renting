using HouseBuyingOrRenting.Domain;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class BoardingHouseRepository : BaseRepository<BoardingHouse>, IBoardingHouseRepository
    {
        public BoardingHouseRepository(MyDbContext db) : base(db, db.BoardingHouses)
        {
        }
    }
}
