using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly MyDbContext _db;

        public UserRepository(MyDbContext db) : base(db.Users)
        {
            _db = db;
        }
    }
}
