using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        public DistrictRepository(MyDbContext db) : base(db, db.Districts)
        {
        }
    }
}
