using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
