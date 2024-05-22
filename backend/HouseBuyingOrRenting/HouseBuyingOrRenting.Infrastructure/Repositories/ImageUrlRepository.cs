using HouseBuyingOrRenting.Domain;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class ImageUrlRepository : BaseRepository<ImageUrl>, IImageUrlRepository
    {
        public ImageUrlRepository(MyDbContext db) : base(db, db.ImageUrls)
        {
        }
    }
}
