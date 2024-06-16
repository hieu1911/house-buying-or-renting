using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HouseBuyingOrRenting.Domain;
using HouseBuyingOrRenting.Application;

namespace HouseBuyingOrRenting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : BaseController<User, UserDto, UserCreateDto, UserUpdateDto>
    {
        private readonly IUserService _userService;

        private readonly ChatHub _chatHub;

        public AuthController(IUserService userService, ChatHub chatHub) : base(userService)
        {
            _userService = userService;
            _chatHub = chatHub;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var user = await _userService.GetUserAsync(userLoginDto);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim("Email", user.Email ?? ""),
                new Claim("PhoneNumber", user.PhoneNumber ?? ""),
                new Claim("Id", user.Id.ToString() ?? ""),
                new Claim("FullName", user.FullName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = userLoginDto.RememberLogin
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return StatusCode(StatusCodes.Status200OK, user);
        }

        [HttpPost]
        [Route("sign-out")]
        public async Task<IActionResult> SignOut()
        {
            var user = await _userService.CheckUserLoginedAsync();

            await HttpContext.SignOutAsync(
       CookieAuthenticationDefaults.AuthenticationScheme);

            _chatHub.RemoveFromGroupAsync(user.Id);

            return StatusCode(StatusCodes.Status200OK, user);
        }

        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> GetUserLoginedAsync()
        {
            var user = await _userService.CheckUserLoginedAsync();
            return StatusCode(StatusCodes.Status200OK, user);
        }
    }
}
