﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByLoginInfo(string phoneOrEmail, string password);

        Task<int> ChangeRole(Guid id);
    }
}
