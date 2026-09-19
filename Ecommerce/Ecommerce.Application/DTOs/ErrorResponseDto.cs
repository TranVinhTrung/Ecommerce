using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs
{
    public class ErrorResponseDto
    {
        public string Message { get; set; } = string.Empty;

        public Dictionary<string, string[]>? Errors { get; set; }
    }
}
