using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities
{
    public class Payment
    {
        public const string Pending = "Pending";
        public const string Paid = "Paid";
        public const string Failed = "Failed";


        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string Status { get; set; } = Pending;

        public string? TransactionId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
