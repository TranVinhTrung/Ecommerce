using Azure.Core;
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
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IJwtTokenService jwtTokenService, 
            IRefreshTokenService refreshTokenService
            )
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _refreshTokenService = refreshTokenService;
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

            if (!result.Succeeded)
                return false;

            await _userManager.AddToRoleAsync(user, "User");

            return true;
        }

        public async Task<TokenResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null)
                return null;

            var result = await _userManager.CheckPasswordAsync(
                user,
                dto.Password);

            if (!result)
                return null;

            var accessToken = await _jwtTokenService.GenerateToken(
                user.Id,
                user.UserName!);

            var refreshToken = await _refreshTokenService.CreateAsync(
                user.Id);

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenResponseDto?> RefreshTokenAsync(string refreshToken)
        {
            var userId = await _refreshTokenService.GetUserIdAsync(refreshToken);

            if (userId == null)
                return null;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            var newRefreshToken = await _refreshTokenService.RotateAsync(refreshToken);

            if (newRefreshToken == null)
                return null;

            var accessToken = await _jwtTokenService.GenerateToken(
                user.Id,
                user.UserName!);

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            return await _refreshTokenService.RevokeAsync(refreshToken);
        }
    }
}
