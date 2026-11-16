using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface ICategoriesGroupRepository
    {
        Task<IEnumerable<CategoriesGroup>> GetAllAsync();

        Task<CategoriesGroup?> GetByIdAsync(int id);

        Task<CategoriesGroup> AddAsync(CategoriesGroup categoriesGroup);

        Task<bool> UpdateAsync(CategoriesGroup categoriesGroup);

        Task<bool> DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}
