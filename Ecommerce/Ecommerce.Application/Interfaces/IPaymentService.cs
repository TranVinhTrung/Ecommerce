using Ecommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> CreatePaymentAsync(string userId, int orderId, string paymentMethod);
        Task<PaymentResponseDto> ProcessPaymentAsync(string userId, int paymentId);
        Task<PaymentResponseDto> FailPaymentAsync(string userId, int paymentId);
    }
}
