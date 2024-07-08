using AutoMapper;
using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

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
            entityCreateDto.Role = (int)UserRole.NORMAL;
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

        public async override Task<User> MapEntityUpdateDtoToEntity(Guid id, UserUpdateDto entityUpdateDto)
        {
            var user = _mapper.Map<User>(entityUpdateDto);
            user.Id = id;
            return user;
        }

        public async Task<UserDto> CheckUserLoginedAsync()
        {
            var context = new HttpContextAccessor().HttpContext;

            var userClaim = context.User;
            if (!userClaim.Identity.IsAuthenticated)
            {
                return null;
            }

            var userDto = new UserDto()
            {
                UserName = userClaim.FindFirst(ClaimTypes.Name).Value,
                Email = userClaim.FindFirst("Email").Value,
                Id = new Guid(userClaim.FindFirst("Id").Value),
                FullName = userClaim.FindFirst("FullName").Value,
                Role = int.Parse(userClaim.FindFirst("Role").Value),
                PhoneNumber = userClaim.FindFirst("PhoneNumber").Value
            };

            return userDto;
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

        public bool IsPasswordValid(string password)
        {
            var pattern = @"^(?=.*[a-zA-Z])(?=.*\d).{6,}$";

            return Regex.IsMatch(password, pattern);
        }

        public async Task<int> ChangeRole(Guid id)
        {
            var result = await _userRepository.ChangeRole(id);
            return result;
        }
    }
}
