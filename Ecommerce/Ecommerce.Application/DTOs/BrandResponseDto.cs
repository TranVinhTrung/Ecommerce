using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs
{
    public class BrandResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? LogoUrl { get; set; }

        public List<int> CategoryIds { get; set; } = new();

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
