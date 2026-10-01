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
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<CartResponseDto> GetCartAsync(string userId)
        {
            var cart = await _cartRepository.GetByUserIdAsync(userId);

            if (cart == null)
            {
                return new CartResponseDto();
            }

            return new CartResponseDto
            {
                Id = cart.Id,

                Items = cart.Items.Select(item => new CartItemResponseDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Subtotal = item.UnitPrice * item.Quantity
                }).ToList(),

                TotalItems = cart.Items.Sum(item => item.Quantity),

                TotalAmount = cart.Items.Sum(item => item.UnitPrice * item.Quantity)
            };
        }

        public async Task<CartResponseDto> AddToCartAsync(string userId, CartItemCreateDto dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new BusinessException("Product not found.");

            if (dto.Quantity > product.Stock)
                throw new BusinessException("Quantity exceeds available stock.");

            var cart = await _cartRepository.GetByUserIdAsync(userId);

            // Nếu User chưa có Cart thì tạo Cart
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };

                cart = await _cartRepository.AddAsync(cart);
            }

            //Kiểm tra Product đã có trong Cart chưa
            var existingItem = cart.Items
                .FirstOrDefault(item => item.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                var newQuantity = existingItem.Quantity + dto.Quantity;

                if (newQuantity > product.Stock)
                    throw new BusinessException("Quantity exceeds available stock.");

                existingItem.Quantity = newQuantity;
                existingItem.UpdatedAt = DateTime.UtcNow;

                await _cartRepository.UpdateAsync(cart);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = product.Id,
                    Quantity = dto.Quantity,
                    UnitPrice = product.Price,
                    CreatedAt = DateTime.UtcNow
                };

                await _cartRepository.AddItemAsync(cartItem);
            }

            return await GetCartAsync(userId);
        }

        public async Task<CartResponseDto> UpdateQuantityAsync(string userId, int cartItemId, int quantity)
        {
            var cart = await _cartRepository.GetByUserIdAsync(userId);

            if(cart == null)
                throw new BusinessException("Cart not found.");

            var cartItem = cart.Items.FirstOrDefault(item => item.Id == cartItemId);

            if (cartItem == null)
                throw new BusinessException("Cart item not found.");

            if (quantity > cartItem.Product.Stock)
                throw new BusinessException("Quantity exceeds available stock.");

            cartItem.Quantity = quantity;
            cartItem.UpdatedAt = DateTime.UtcNow;

            await _cartRepository.UpdateAsync(cart);

            return await GetCartAsync(userId);
        }

        public async Task<CartResponseDto> RemoveItemAsync(string userId, int cartItemId)
        {
            var result = await _cartRepository.RemoveItemAsync(userId, cartItemId);

            if (!result)
                throw new BusinessException("Cart item not found.");

            return await GetCartAsync(userId);
        }
    }
}
