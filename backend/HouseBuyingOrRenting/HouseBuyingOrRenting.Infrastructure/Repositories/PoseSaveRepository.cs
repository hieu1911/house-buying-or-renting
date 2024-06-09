using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class PoseSaveRepository : BaseRepository<PostSave>, IPostSaveRepository
    {
        private readonly MyDbContext _db;

        public PoseSaveRepository(MyDbContext db) : base(db, db.PostSaves)
        {
            _db = db;
        }

        public async Task<PostSave> GetBuyUserIdAndRealEstateId(Guid userId, Guid realEstateId)
        {
            var result = _db.PostSaves.SingleOrDefault(p => p.UserId == userId && p.RealEstateId == realEstateId);
            return result;
        }

        public async Task<List<Guid>> GetRealEstateIdsByUser(Guid userId)
        {
            var result = _db.PostSaves.Where(p => p.UserId == userId).Select(p => p.RealEstateId).ToList();
            return result;
        }
    }
}
