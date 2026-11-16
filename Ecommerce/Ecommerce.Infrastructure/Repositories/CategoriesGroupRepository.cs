

using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Ecommerce.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CategoriesGroupRepository : ICategoriesGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriesGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoriesGroup>> GetAllAsync()
        {
            return await _context.CategoriesGroups.ToListAsync();
        }

        public async Task<CategoriesGroup?> GetByIdAsync(int id)
        {
            return await _context.CategoriesGroups.FindAsync(id);
        }

        public async Task<CategoriesGroup> AddAsync(CategoriesGroup categoriesGroup)
        {
            _context.CategoriesGroups.Add(categoriesGroup);
            await _context.SaveChangesAsync();

            return categoriesGroup;
        }

        public async Task<bool> UpdateAsync(CategoriesGroup categoriesGroup)
        {
            var existingCategoriesGroup =
                await _context.CategoriesGroups.FindAsync(categoriesGroup.Id);

            if (existingCategoriesGroup == null)
                return false;

            existingCategoriesGroup.Name = categoriesGroup.Name;
            existingCategoriesGroup.Description = categoriesGroup.Description;
            existingCategoriesGroup.Icon = categoriesGroup.Icon;
            existingCategoriesGroup.DisplayOrder = categoriesGroup.DisplayOrder;
            existingCategoriesGroup.IsActive = categoriesGroup.IsActive;
            existingCategoriesGroup.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoriesGroup = await _context.CategoriesGroups.FindAsync(id);

            if (categoriesGroup == null)
                return false;

            _context.CategoriesGroups.Remove(categoriesGroup);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.CategoriesGroups
                .AnyAsync(c => c.Id == id);
        }
    }
}