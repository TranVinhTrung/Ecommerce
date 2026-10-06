using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Ecommerce.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment?> GetByIdAsync(int paymentId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.Id == paymentId);
        }

        public async Task<Payment?> GetByOrderIdAsync(int orderId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        public async Task<Payment> AddAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

       
    }
}
