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
                ParentCategoryId = category.ParentCategoryId,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            });
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetParentCategoriesAsync()
        {
            var categories = await _repository.GetParentCategoriesAsync();

            return categories.Select(category => new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ParentCategoryId = category.ParentCategoryId,
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
                ParentCategoryId = category.ParentCategoryId,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }

        public async Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto)
        {
            if (dto.ParentCategoryId.HasValue)
            {
                var parentExists = await _repository.ExistsAsync(
                    dto.ParentCategoryId.Value);

                if (!parentExists)
                {
                    throw new BusinessException(
                        $"Parent category with ID {dto.ParentCategoryId.Value} not found.");
                }
            }

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                ParentCategoryId = dto.ParentCategoryId,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            };

            var created = await _repository.AddAsync(category);

            return new CategoryResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                ParentCategoryId = created.ParentCategoryId,
                CreatedAt = created.CreatedAt
            };
        }


        public async Task<bool> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var existingCategory = await _repository.GetByIdAsync(id);

            if (existingCategory == null)
                return false;

            if (dto.ParentCategoryId.HasValue)
            {
                if (dto.ParentCategoryId.Value == id) throw new BusinessException( "A category cannot be its own parent.");

                var parentExists = await _repository.ExistsAsync(
                    dto.ParentCategoryId.Value);

                if (!parentExists)
                    throw new BusinessException(
                        "Parent category does not exist.");

                var categories = await _repository.GetAllAsync();
                var categoryLookup = categories.ToDictionary(c => c.Id);

                var currentParentId = dto.ParentCategoryId;

                while (currentParentId.HasValue)
                {
                    if (currentParentId.Value == id)
                        throw new BusinessException(
                            "Circular category hierarchy is not allowed.");

                    if (!categoryLookup.TryGetValue(
                            currentParentId.Value, out var parent))
                        break;

                    currentParentId = parent.ParentCategoryId;
                }
            }

            var category = new Category
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                ParentCategoryId = dto.ParentCategoryId,
                UpdatedAt = DateTime.UtcNow
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
