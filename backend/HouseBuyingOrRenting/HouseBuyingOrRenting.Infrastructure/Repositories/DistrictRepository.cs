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
        private readonly MyDbContext _db;

        public DistrictRepository(MyDbContext db) : base(db, db.Districts)
        {
            _db = db;
        }

        public async Task<List<District>> GetDistrictsByProvinceId(Guid provinceId)
        {
            var districts = _db.Districts.Where(district => district.ProvinceId == provinceId).ToList();
            return districts;
        }
    }
}
