using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await serviceManager.AuthService.LoginAsync(loginDto);
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await serviceManager.AuthService.RegisterAsync(registerDto);
            return Ok(result);
        }

        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var user = await serviceManager.AuthService.CheckEmailExistsAsync(email);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);


        }
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await serviceManager.AuthService.GetCurrentUserAsync(email);

            return Ok(user);
        }
        [HttpGet("Address")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await serviceManager.AuthService.GetCurrentUserAddressAsync(email);

            return Ok(user);
        }
        [HttpPut("Address")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await serviceManager.AuthService.UpdateCurrentUserAddressAsync(address , email);

            return Ok(user);
        }
    }
}