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
    public class ProductImageService : IProductImageService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _imageRepository;

        public ProductImageService(IProductRepository productRepository, IProductImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
        }

        public async Task<bool> AddAsync(int productId, string imageUrl, bool isPrimary)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            //Kiểm tra
            if (product == null)
                return false;

            if (isPrimary)
            {
                foreach(var existingImage in product.Images)
                {
                    existingImage.IsPrimary = false;
                }
            }

            var image = new ProductImage
            {
                ProductId = productId,
                ImageUrl = imageUrl,
                IsPrimary = isPrimary
            };

            await _imageRepository.AddAsync(image);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _imageRepository.DeleteAsync(id);
        }
    }
}
