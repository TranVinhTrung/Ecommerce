using Ecommerce.Application.Interfaces;
using Ecommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Identity
{
    public class RefreshTokenService : IRefreshTokenService
    {

        private readonly ApplicationDbContext _context;

        public RefreshTokenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateAsync(string userId)
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            var token = Convert.ToBase64String(randomBytes);

            var refreshToken = new RefreshToken 
            {
                Token = token,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return token;
        }

        public async Task<string?> GetUserIdAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
           .FirstOrDefaultAsync(x => x.Token == token);

            //Kiểm tra xem refresh token có tồn tại, hết hạn hay đã bị thu hồi hay không
            if (refreshToken == null)
                return null;

            if (refreshToken.ExpiresAt <= DateTime.UtcNow)
                return null;

            if (refreshToken.RevokedAt != null)
                return null;

            return refreshToken.UserId;
        }

        public async Task<bool> RevokeAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);

            if (refreshToken == null)
                return false;

            if (refreshToken.RevokedAt != null)
                return false;

            refreshToken.RevokedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string?> RotateAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);

            if (refreshToken == null)
                return null;

            if (refreshToken.ExpiresAt <= DateTime.UtcNow)
                return null;

            if (refreshToken.RevokedAt != null)
                return null;

            refreshToken.RevokedAt = DateTime.UtcNow;

            var newToken = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));

            var newRefreshToken = new RefreshToken
            {
                Token = newToken,
                UserId = refreshToken.UserId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            _context.RefreshTokens.Add(newRefreshToken);

            await _context.SaveChangesAsync();

            return newToken;
        }

    }
}
