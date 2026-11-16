using Ecommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface ICategoriesGroupService
    {
        Task<IEnumerable<CategoriesGroupResponseDto>> GetAllAsync();

        Task<CategoriesGroupResponseDto?> GetByIdAsync(int id);

        Task<CategoriesGroupResponseDto> CreateAsync(CategoriesGroupCreateDto dto);

        Task<bool> UpdateAsync(int id, CategoriesGroupUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
