﻿using Microsoft.AspNetCore.Authentication.Cookies;
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
    public class AuthController : BaseController<User>
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var user = await _userService.GetUserAsync(userLoginDto);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Email", user.Email ?? ""),
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
    }
}
