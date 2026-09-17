using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs
{
    public class PagedResultDto<T>
    {
        /*Tại sao dùng <T>? T nghĩa là kiểu dữ liệu bất kỳ.*/

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<T> Items { get; set; } = [];
    }
}
