using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<ImageUrl> ImageUrls { get; set; }

        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<House> Houses { get; set; }

        public DbSet<BoardingHouse> BoardingHouses { get; set; }

        public DbSet<Land> Lands { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<RealEstate> RealEstates { get; set; }
    }
}
