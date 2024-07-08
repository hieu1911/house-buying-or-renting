using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly MyDbContext _db;

        public UserRepository(MyDbContext db) : base(db, db.Users)
        {
            _db = db;
        }

        public async Task<int> ChangeRole(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                if (user.Role == 2) user.Role = 3;
                else user.Role = 2;

                await _db.SaveChangesAsync();

                return 1;
            }

            return 0;
        }

        public async Task<User?> GetUserByLoginInfo(string phoneOrEmail, string password)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => (u.PhoneNumber == phoneOrEmail || u.Email == phoneOrEmail)
                && u.Password == password);

            return user;
        }

        public override async Task<int> UpdateAsync(User entity)
        {
            var user = await _db.Users.FindAsync(entity.Id);
            if (user != null) 
            {
                user.FullName = entity.FullName;
                user.Email = entity.Email;
                user.PhoneNumber = entity.PhoneNumber;

                await _db.SaveChangesAsync();
                return 1;
            }

            return 0;
        }
    }
}
