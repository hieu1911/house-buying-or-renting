using HouseBuyingOrRenting.Domain;
using HouseBuyingOrRenting.Domain.Enums;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Address>> GetDistrictsName()
        {
            var result = _db.Districts.Select(x => new
            {
                x.Id,
                x.Name
            });

            return result.Select(r => new Address() { Id = r.Id, Name = r.Name, Type = AddressType.DISTRICT }).ToList();
        }
    }
}
