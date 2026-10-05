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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }
        public async Task<OrderResponseDto> CreateOrderAsync(string userId)
        {
            var cart = await _cartRepository.GetByUserIdAsync(userId);

            if (cart == null || !cart.Items.Any())
                throw new BusinessException("Cart is empty.");

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Status = Order.Pending
            };

            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    ProductName = cartItem.Product.Name,
                    UnitPrice = cartItem.UnitPrice,
                    Quantity = cartItem.Quantity,
                    Subtotal = cartItem.UnitPrice * cartItem.Quantity
                };

                order.Items.Add(orderItem);
            }

            order.TotalAmount = order.Items.Sum(item => item.Subtotal);

            Order? createdOrder = null;

            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                // Kiểm tra và cập nhật số lượng tồn kho của sản phẩm trước khi tạo đơn hàng
                foreach (var cartItem in cart.Items)
                {
                    var product = await _productRepository.GetByIdForUpdateAsync(cartItem.ProductId);

                    if (product == null)
                        throw new BusinessException("Product not found.");

                    if (cartItem.Quantity > product.Stock)
                        throw new BusinessException(
                            $"Product '{product.Name}' does not have enough stock.");

                    product.Stock -= cartItem.Quantity;
                }


                createdOrder = await _orderRepository.AddAsync(order);

                await _cartRepository.ClearAsync(userId);
            });

            return MapToDto(createdOrder);

        }        

        public async Task<List<OrderResponseDto>> GetOrdersAsync(string userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);

            return orders.Select(MapToDto).ToList();
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(string userId, int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(userId, orderId);

            if (order == null)
                return null;

            return MapToDto(order);
        }

        private OrderResponseDto MapToDto(Order order)
        {
            return new OrderResponseDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = order.CreatedAt,

                Items = order.Items.Select(item => new OrderItemResponseDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Subtotal = item.Subtotal
                }).ToList()
            };
        }

        public async Task<bool> UpdateOrderStatusAsync(string userId, int orderId, string status)
        {
            var validStatuses = new[] { Order.Pending, Order.Confirmed, Order.Shipping, Order.Completed, Order.Cancelled };

            if (!validStatuses.Contains(status))
                throw new BusinessException("Invalid order status.");


            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
                return false;

            var isValidTransition = order.Status switch
            {
                Order.Pending => status == Order.Confirmed || status == Order.Cancelled,
                Order.Confirmed => status == Order.Shipping,
                Order.Shipping => status == Order.Completed,
                _ => false
            };

            if (!isValidTransition)
                throw new BusinessException(
                    $"Cannot change order status from '{order.Status}' to '{status}'.");


            order.Status = status;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);

            return true;
        }
    }
}
