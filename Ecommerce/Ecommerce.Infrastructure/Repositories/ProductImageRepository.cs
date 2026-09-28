using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Ecommerce.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductImage> AddAsync(ProductImage image)
        {
            _context.ProductImages.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var image = await _context.ProductImages.FindAsync(id);
            if (image == null)
                return false;
            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
