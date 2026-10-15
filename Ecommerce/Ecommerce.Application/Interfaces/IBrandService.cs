using Ecommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandResponseDto>> GetAllAsync();

        Task<BrandResponseDto?> GetByIdAsync(int id);

        Task<BrandResponseDto> CreateAsync(BrandCreateDto dto, string? logoUrl);

        Task<bool> UpdateAsync(int id, BrandUpdateDto dto, string? logoUrl );

        Task<bool> DeleteAsync(int id);
    }
}
