using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ImageUrl> ImageUrls { get; set; }

        public DbSet<RealEstate> RealEstates { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<District> Districts { get; set; }
    }
}
