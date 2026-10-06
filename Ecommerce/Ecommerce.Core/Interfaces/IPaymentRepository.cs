using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByIdAsync(int paymentId);
        Task<Payment?> GetByOrderIdAsync(int orderId);
        Task<Payment> AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
    }
}
