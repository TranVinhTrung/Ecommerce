using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.Core.Interfaces
{
    public interface IProductRepository
    {
        // Dùng IQueryable thay GetAllAsync() vì query chưa được thực thi ngay.
        // Service có thể tiếp tục Where/OrderBy/Skip/Take rồi EF Core mới chuyển
        // thành SQL và lấy đúng dữ liệu cần thiết từ Database.
        IQueryable<Product> GetQuery();

        Task<Product?> GetByIdAsync(int id);

        Task<Product> AddAsync(Product product);

        Task<bool> UpdateAsync(Product product);

        Task<bool> DeleteAsync(int id);

        Task<bool> ExistsByCategoryIdAsync(int categoryId);
    }
}
