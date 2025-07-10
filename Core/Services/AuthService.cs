using AutoMapper;
using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthService(UserManager<AppUser> userManager, IOptions<JwtOptions> options , IMapper mapper) : IAuthService
    {
        

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new UnAuthorizedException();
            }
            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                throw new UnAuthorizedException("Invalid password");
            }
            return new UserResultDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateTokenAsync(user),

            };
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await CheckEmailExistsAsync(registerDto.Email)) {
                throw new DuplicatedEmailBadRequestException(registerDto.Email);
            }
            var user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return new UserResultDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateTokenAsync(user),

            };
        }
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user != null;
        }
        public async Task<UserResultDto> GetCurrentUserAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null) throw new UserNotFoundException(email);
            return new UserResultDto ()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateTokenAsync(user),

            };
        }
        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
                
             if (user is null)throw new UserNotFoundException(email);
             var result =  mapper.Map<AddressDto>(user.Address);
             return result;


        }

        
        public async Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto address, string email)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);

            if (user is null) throw new UserNotFoundException(email);
            if (user.Address is not null) 
            {
                user.Address.FirstName = address.FirstName;
                user.Address.LastName = address.LastName;
                user.Address.Street = address.Street;
                user.Address.City = address.City;
                user.Address.Country = address.Country;
            }
            else
            {
               var addressResult =  mapper.Map<Address>(address);
                user.Address = addressResult ;
            }
             await userManager.UpdateAsync(user);


            return address;
        }

        private async Task<string> GenerateTokenAsync(AppUser user)
        {
            var jwtOptions = options.Value;
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
               
            };
            // Add more claims as needed
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Define the signing key (this should be stored securely in production)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)); // Replace with your actual secret key
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: authClaims, // Add claims as needed
                expires: DateTime.UtcNow.AddHours(jwtOptions.DurationInDays),
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature) // Add signing credentials as needed
                );
            // Token generation logic goes here
            // This is a placeholder for the actual token generation logic
            return new JwtSecurityTokenHandler().WriteToken(token);
        }








    }
}
