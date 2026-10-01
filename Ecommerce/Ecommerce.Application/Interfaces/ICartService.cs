using Ecommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface ICartService
    {
        Task<CartResponseDto> GetCartAsync(string userId);
        Task<CartResponseDto> AddToCartAsync(string userId, CartItemCreateDto dto);
        Task<CartResponseDto> UpdateQuantityAsync(string userId, int cartItemId, int quantity);
        Task<CartResponseDto> RemoveItemAsync(string userId, int cartItemId);
    }
}
