using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByNameAsync(dto.UserName);

            if (existingUser != null)
                return false;

            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(
                user,
                dto.Password);

            return result.Succeeded;
        }

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null)
                return null;

            var result = await _userManager.CheckPasswordAsync(
                user,
                dto.Password);

            if (!result)
                return null;

            var token = _jwtTokenService.GenerateToken(
                user.Id,
                user.UserName!);

            return token;
        }
    }
}
