using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs
{
    public class ProductImageCreateDto
    {
        [Required]
        [Url]
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }  //xác định ảnh có phải ảnh chính hay không
    }
}
