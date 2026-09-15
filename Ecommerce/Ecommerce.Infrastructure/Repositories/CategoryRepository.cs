using Ecommerce.Application.Interfaces;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Ecommerce.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            /*
            Không xài _context.Categories.Update(category); vì tránh trường hợp category được attach vào context
            nhưng không phải là entity tracked, dẫn đến việc update không thành công. 
            Thay vào đó, tìm kiếm entity hiện có trong context và cập nhật các thuộc tính của nó.
            */

            var existingCategory = await _context.Categories.FindAsync(category.Id);

            if (existingCategory == null)
                return false;

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        
    }
}
