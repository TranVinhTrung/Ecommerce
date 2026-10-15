using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Danh mục cha
        public int? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }

        // Các danh mục con
        public ICollection<Category> SubCategories { get; set; } = new List<Category>();

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<Product> Products { get; set; }
            = new List<Product>();

        // Quan hệ nhiều-nhiều với Brand
        public ICollection<Brand> Brands { get; set; } = new List<Brand>();
    }
}
