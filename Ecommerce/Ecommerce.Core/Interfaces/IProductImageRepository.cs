using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface IProductImageRepository
    {
        Task<ProductImage> AddAsync(ProductImage image);

        Task<bool> DeleteAsync(int id);
    }
}
