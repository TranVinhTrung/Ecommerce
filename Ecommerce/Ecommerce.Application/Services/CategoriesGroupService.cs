using Ecommerce.Application.DTOs;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Interfaces;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;

namespace Ecommerce.Application.Services
{
    public class CategoriesGroupService : ICategoriesGroupService
    {
        private readonly ICategoriesGroupRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesGroupService(
            ICategoriesGroupRepository repository,
            ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoriesGroupResponseDto>> GetAllAsync()
        {
            var groups = await _repository.GetAllAsync();

            return groups.Select(group => new CategoriesGroupResponseDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                Icon = group.Icon,
                DisplayOrder = group.DisplayOrder,
                IsActive = group.IsActive,
                CreatedAt = group.CreatedAt,
                UpdatedAt = group.UpdatedAt
            });
        }

        public async Task<CategoriesGroupResponseDto?> GetByIdAsync(int id)
        {
            var group = await _repository.GetByIdAsync(id);

            if (group == null)
                return null;

            return new CategoriesGroupResponseDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                Icon = group.Icon,
                DisplayOrder = group.DisplayOrder,
                IsActive = group.IsActive,
                CreatedAt = group.CreatedAt,
                UpdatedAt = group.UpdatedAt
            };
        }

        public async Task<CategoriesGroupResponseDto> CreateAsync(
            CategoriesGroupCreateDto dto)
        {
            var group = new CategoriesGroup
            {
                Name = dto.Name,
                Description = dto.Description,
                Icon = dto.Icon,
                DisplayOrder = dto.DisplayOrder,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            var created = await _repository.AddAsync(group);

            return new CategoriesGroupResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                Icon = created.Icon,
                DisplayOrder = created.DisplayOrder,
                IsActive = created.IsActive,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<bool> UpdateAsync(
            int id,
            CategoriesGroupUpdateDto dto)
        {
            var existingGroup = await _repository.GetByIdAsync(id);

            if (existingGroup == null)
                return false;

            var group = new CategoriesGroup
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                Icon = dto.Icon,
                DisplayOrder = dto.DisplayOrder,
                IsActive = dto.IsActive,
                UpdatedAt = DateTime.UtcNow
            };

            return await _repository.UpdateAsync(group);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var groupExists = await _repository.ExistsAsync(id);

            if (!groupExists)
                return false;

            var categories = await _categoryRepository.GetAllAsync();

            var hasCategories = categories.Any(c => c.CategoryGroupId == id);

            if (hasCategories)
            {
                throw new BusinessException(
                    "Cannot delete categories group because it has categories.");
            }

            return await _repository.DeleteAsync(id);
        }
    }
}