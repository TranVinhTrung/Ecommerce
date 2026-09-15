using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

        Task<Category> AddAsync(Category category);

        Task<bool> UpdateAsync(Category category);

        Task<bool> DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}
