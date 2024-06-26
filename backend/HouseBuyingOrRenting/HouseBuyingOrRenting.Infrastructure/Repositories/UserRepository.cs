﻿using HouseBuyingOrRenting.Domain;
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

        public async Task<User?> GetUserByLoginInfo(string phoneOrEmail, string password)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => (u.PhoneNumber == phoneOrEmail || u.Email == phoneOrEmail)
                && u.Password == password);

            return user;
        }
    }
}
