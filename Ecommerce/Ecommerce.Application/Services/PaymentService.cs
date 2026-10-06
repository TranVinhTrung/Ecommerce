using Ecommerce.Application.DTOs;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Interfaces;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;

        }
        public async Task<PaymentResponseDto> CreatePaymentAsync(string userId, int orderId, string paymentMethod)
        {
            var order = await _orderRepository.GetByIdAsync(userId, orderId);

            if (order == null)
                throw new BusinessException("Order not found.");

            if (order.Status != Order.Pending)
                throw new BusinessException("Only pending orders can be paid.");

            var existingPayment = await _paymentRepository.GetByOrderIdAsync(orderId);

            if (existingPayment != null)
                throw new BusinessException("Payment already exists for this order.");


            var payment = new Payment
            {
                OrderId = order.Id,
                Amount = order.TotalAmount,
                PaymentMethod = paymentMethod,
                Status = Payment.Pending,
                TransactionId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow
            };

            var createdPayment = await _paymentRepository.AddAsync(payment);

            return new PaymentResponseDto
            {
                Id = createdPayment.Id,
                OrderId = createdPayment.OrderId,
                Amount = createdPayment.Amount,
                PaymentMethod = createdPayment.PaymentMethod,
                Status = createdPayment.Status,
                TransactionId = createdPayment.TransactionId,
                CreatedAt = createdPayment.CreatedAt
            };
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(string userId, int paymentId)
        {
            var payment = await _paymentRepository.GetByIdAsync(paymentId);

            if (payment == null)
                throw new BusinessException("Payment not found.");

            var order = await _orderRepository.GetByIdAsync(userId, payment.OrderId);

            if (order == null)
                throw new BusinessException("Payment not found.");

            if (payment.Status != Payment.Pending)
                throw new BusinessException("Payment has already been processed.");

            if (order.Status != Order.Pending)
                throw new BusinessException("Order is not pending.");

            payment.Status = Payment.Paid;
            payment.UpdatedAt = DateTime.UtcNow;

            order.Status = Order.Confirmed;
            order.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _paymentRepository.UpdateAsync(payment);
                await _orderRepository.UpdateAsync(order);
            });

            return new PaymentResponseDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status,
                TransactionId = payment.TransactionId,
                CreatedAt = payment.CreatedAt
            };
        }

        public async Task<PaymentResponseDto> FailPaymentAsync(string userId, int paymentId)
        {
            var payment = await _paymentRepository.GetByIdAsync(paymentId);

            if (payment == null)
                throw new BusinessException("Payment not found.");

            var order = await _orderRepository.GetByIdAsync(userId, payment.OrderId);

            if (order == null)
                throw new BusinessException("Payment not found.");

            if (payment.Status != Payment.Pending)
                throw new BusinessException("Payment has already been processed.");

            payment.Status = Payment.Failed;
            payment.UpdatedAt = DateTime.UtcNow;

            await _paymentRepository.UpdateAsync(payment);

            return new PaymentResponseDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status,
                TransactionId = payment.TransactionId,
                CreatedAt = payment.CreatedAt
            };
        }


    }
}
