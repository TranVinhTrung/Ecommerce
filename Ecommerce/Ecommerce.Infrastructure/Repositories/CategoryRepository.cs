using Ecommerce.Application.Interfaces;
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
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
