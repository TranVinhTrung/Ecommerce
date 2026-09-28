using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IProductImageService
    {
        Task<bool> AddAsync(int productId, string imageUrl, bool isPrimary);

        Task<bool> DeleteAsync(int id);
    }
}
