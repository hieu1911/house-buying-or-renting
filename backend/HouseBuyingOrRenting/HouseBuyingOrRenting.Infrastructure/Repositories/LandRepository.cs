using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class LandRepository : BaseRepository<Land>, ILandRepository
    {
        public LandRepository(MyDbContext db) : base(db, db.Lands)
        {
        }
    }
}
