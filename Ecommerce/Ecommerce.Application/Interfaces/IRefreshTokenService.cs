using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> CreateAsync(string userId);

        Task<string?> GetUserIdAsync(string token);
        Task<bool> RevokeAsync(string token);
        Task<string?> RotateAsync(string token);
    }
}
