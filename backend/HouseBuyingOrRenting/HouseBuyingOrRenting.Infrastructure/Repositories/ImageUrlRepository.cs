using HouseBuyingOrRenting.Domain;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class ImageUrlRepository : BaseRepository<ImageUrl>, IImageUrlRepository
    {
        private readonly MyDbContext _db;

        public ImageUrlRepository(MyDbContext db) : base(db, db.ImageUrls)
        {
            _db = db;
        }
    }
}
