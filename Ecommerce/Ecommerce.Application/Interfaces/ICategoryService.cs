using Ecommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllAsync();

        Task<CategoryResponseDto?> GetByIdAsync(int id);

        Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto);

        Task<bool> UpdateAsync(int id, CategoryUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
