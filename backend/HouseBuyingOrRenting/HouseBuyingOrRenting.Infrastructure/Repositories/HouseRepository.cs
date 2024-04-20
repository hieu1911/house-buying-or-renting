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
        public HouseRepository(MyDbContext db) : base(db, db.Houses)
        {
        }
    }
}
