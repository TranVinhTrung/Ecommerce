
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Ecommerce.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.Brands
                .Include(b => b.Categories)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            return await _context.Brands
                .Include(b => b.Categories)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Brand> AddAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<bool> UpdateAsync(Brand brand)
        {
            var existingBrand = await _context.Brands
                .Include(b => b.Categories)
                .FirstOrDefaultAsync(b => b.Id == brand.Id);

            if (existingBrand == null)
                return false;

            existingBrand.Name = brand.Name;
            existingBrand.LogoUrl = brand.LogoUrl;
            existingBrand.UpdatedAt = DateTime.UtcNow;

            var incomingCategories = brand.Categories.ToList();
            existingBrand.Categories.Clear();


            foreach (var category in incomingCategories)
            {
                var existingCategory = await _context.Categories
                    .FindAsync(category.Id);

                if (existingCategory != null)
                    existingBrand.Categories.Add(existingCategory);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
                return false;

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Brands.AnyAsync(b => b.Id == id);
        }
    }
}
