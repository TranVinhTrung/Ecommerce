using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);

        Task<List<Order>> GetByUserIdAsync(string userId);

        Task<Order?> GetByIdAsync(string userId, int orderId);

        Task<Order?> GetByIdAsync(int orderId);  //dành cho admin lấy order không bị giói hạn user

        Task UpdateAsync(Order order);
    }
}
