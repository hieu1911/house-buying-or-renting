using AutoMapper;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System.Text;

namespace HouseBuyingOrRenting.Application
{
    public class UserService : BaseService<User, UserDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly HashPasswordOptions _options;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IOptions<HashPasswordOptions> options) : base(userRepository)
        {
            _userRepository = userRepository;
            _options = options.Value;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserAsync(UserLoginDto userLoginDto)
        {
            var passwordHashed = HashPassword(userLoginDto.Password);

            var user = await _userRepository.GetUserByLoginInfo(userLoginDto.PhoneOrEmail, passwordHashed);
            if (user is null)
            {
                throw new UnAuthorizeException();
            }

            var userDto = await MapEntityToEntityDto(user);

            return userDto;
        }

        public override Task<int> InsertAsync(UserCreateDto entityCreateDto)
        {
            entityCreateDto.Password = HashPassword(entityCreateDto.Password);

            return base.InsertAsync(entityCreateDto);
        }

        public async override Task<User> MapEntityCreateDtoToEntity(UserCreateDto entityCreateDto)
        {
            var user = _mapper.Map<User>(entityCreateDto);

            return user;
        }

        public async override Task<UserDto> MapEntityToEntityDto(User entity)
        {
            var userDto = _mapper.Map<UserDto>(entity);
            return userDto;
        }

        public override Task<User> MapEntityUpdateDtoToEntity(Guid id, UserUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }

        private string HashPassword(string password)
        {
            var salt = Encoding.ASCII.GetBytes(_options.Salt);
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
