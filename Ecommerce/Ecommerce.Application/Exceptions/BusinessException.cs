using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Exceptions
{
    public class BusinessException : Exception
    {
        /*
         Tại sao tạo class này?
        Thay vì: throw new ArgumentException("Category does not exist.");
        ta dùng:
        throw new BusinessException("Category does not exist.");
         */

        public BusinessException(string message) : base(message)
        {
        }
    }
}
