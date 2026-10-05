using Ecommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(string userId);

        Task<List<OrderResponseDto>> GetOrdersAsync(string userId);

        Task<OrderResponseDto?> GetOrderByIdAsync(string userId, int orderId);
        Task<bool> UpdateOrderStatusAsync(string userId, int orderId, string status);
    }
}
