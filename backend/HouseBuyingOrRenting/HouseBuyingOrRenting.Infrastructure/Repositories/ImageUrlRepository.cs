using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class ImageUrlRepository : BaseRepository<ImageUrl>, IImageUrlRepository
    {
        public ImageUrlRepository(MyDbContext db) : base(db, db.ImageUrls)
        {
        }
    }
}
