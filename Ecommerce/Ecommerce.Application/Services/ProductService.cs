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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(product => new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            });
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
             var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            };
        }

        public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
        {

            var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);

            if (!categoryExists)
                throw new BusinessException("Category does not exist.");

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.Now
            };


            var created = await _repository.AddAsync(product);

            return new ProductResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                Price = created.Price,
                Stock = created.Stock,
                CategoryId = created.CategoryId
            };
        }

        public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
        {
            var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);

            if (!categoryExists)
                throw new BusinessException("Category does not exist.");


            var product = new Product
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };

            return await _repository.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {   
            return await _repository.DeleteAsync(id);
        }
    }
}
