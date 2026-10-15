using Ecommerce.Application.DTOs;
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
    public class BrandService : IBrandService
    {

        private readonly IBrandRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public BrandService(
            IBrandRepository repository,
            ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<BrandResponseDto>> GetAllAsync()
        {
            var brands = await _repository.GetAllAsync();

            return brands.Select(MapToDto);
        }

        public async Task<BrandResponseDto?> GetByIdAsync(int id)
        {
            var brand = await _repository.GetByIdAsync(id);

            return brand == null ? null : MapToDto(brand);
        }

        public async Task<BrandResponseDto> CreateAsync(BrandCreateDto dto, string? logoUrl)
        {
            var categoryIds = dto.CategoryIds.Distinct().ToList();

            foreach (var categoryId in categoryIds)
            {
                if (!await _categoryRepository.ExistsAsync(categoryId))
                    throw new ArgumentException(
                        $"Category with ID {categoryId} not found.");
            }

            var brand = new Brand
            {
                Name = dto.Name,
                LogoUrl = logoUrl,
                CreatedAt = DateTime.UtcNow,
                Categories = categoryIds
                    .Select(id => new Category { Id = id })
                    .ToList()
            };

            var created = await _repository.AddAsync(brand);
            return MapToDto(created);
        }

        public async Task<bool> UpdateAsync(int id, BrandUpdateDto dto, string? logoUrl)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return false;

            var categoryIds = dto.CategoryIds.Distinct().ToList();

            foreach (var categoryId in categoryIds)
            {
                if (!await _categoryRepository.ExistsAsync(categoryId))
                    throw new ArgumentException(
                        $"Category with ID {categoryId} not found.");
            }

            existing.Name = dto.Name;
            existing.Categories = categoryIds
                .Select(id => new Category { Id = id })
                .ToList();
            existing.UpdatedAt = DateTime.UtcNow;

            if (logoUrl != null)
                existing.LogoUrl = logoUrl;

            return await _repository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _repository.ExistsAsync(id))
                return false;

            return await _repository.DeleteAsync(id);
        }
        

        private static BrandResponseDto MapToDto(Brand brand)
        {
            return new BrandResponseDto
            {
                Id = brand.Id,
                Name = brand.Name,
                LogoUrl = brand.LogoUrl,
                CategoryIds = brand.Categories
                    .Select(category => category.Id)
                    .ToList(),
                CreatedAt = brand.CreatedAt,
                UpdatedAt = brand.UpdatedAt
            };
        }
    }
}
