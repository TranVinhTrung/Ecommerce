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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IProductRepository _productRepository;

        public CategoryService(ICategoryRepository repository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(category => new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            });
        }

        public async Task<CategoryResponseDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }

        public async Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            };

            var created = await _repository.AddAsync(category);

            return new CategoryResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var category = new Category
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                UpdatedAt = DateTime.Now
            };

            return await _repository.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoryExists = await _repository.ExistsAsync(id);

            if (!categoryExists)
                return false;

            var hasProducts = await _productRepository.ExistsByCategoryIdAsync(id);

            if (hasProducts)
                throw new BusinessException("Cannot delete category because it has products.");

            return await _repository.DeleteAsync(id);
        }
    }
}
