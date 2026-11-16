using System;
using System.Collections.Generic;

namespace Ecommerce.Core.Entities
{
    public class CategoriesGroup
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


        // Navigation property - một CategoriesGroup có nhiều Category
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
