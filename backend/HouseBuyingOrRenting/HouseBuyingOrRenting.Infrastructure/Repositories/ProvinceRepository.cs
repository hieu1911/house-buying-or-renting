using HouseBuyingOrRenting.Domain;
using HouseBuyingOrRenting.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
    {
        private readonly DbSet<Province> _dbSet;

        public ProvinceRepository(MyDbContext db) : base(db, db.Provinces)
        {
            _dbSet = db.Provinces;
        }

        public override async Task<List<Province>> GetAllAsync()
        {
            var result = await _dbSet.OrderBy(p => p.Name).ToListAsync();

            return result;
        }

        public async Task<List<Address>> GetProinvcesName()
        {
            var result = _dbSet.Select(x => new
            {
                x.Id,
                x.Name
            });

            return result.Select(r => new Address() { Id = r.Id, Name = r.Name, Type = AddressType.PROVINCE }).ToList();
        }
    }
}
