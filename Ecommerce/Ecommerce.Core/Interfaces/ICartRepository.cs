using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetByUserIdAsync(string userId);
        Task<Cart> AddAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task<CartItem> AddItemAsync(CartItem cartItem);
        Task<bool> RemoveItemAsync(string userId, int cartItemId);
        Task ClearAsync(string userId);

    }
}
