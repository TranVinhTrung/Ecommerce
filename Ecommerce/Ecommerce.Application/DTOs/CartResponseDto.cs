using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs
{
    public class CartResponseDto
    {
        public int Id { get; set; }

        public List<CartItemResponseDto> Items { get; set; } = new();

        public int TotalItems { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
