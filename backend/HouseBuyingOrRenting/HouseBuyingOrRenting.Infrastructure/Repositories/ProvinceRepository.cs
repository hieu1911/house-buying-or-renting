using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
    {
        public ProvinceRepository(MyDbContext db) : base(db, db.Provinces)
        {
        }
    }
}
