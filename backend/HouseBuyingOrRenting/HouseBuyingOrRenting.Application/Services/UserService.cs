using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application.Services
{
    public class UserService : BaseService<User, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> GetUserAsync(UserLoginDto user)
        {
            throw new NotImplementedException();
        }
    }
}
