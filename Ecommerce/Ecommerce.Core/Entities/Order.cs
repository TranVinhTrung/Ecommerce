using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities
{
    public class Order
    {
        public const string Pending = "Pending";
        public const string Confirmed = "Confirmed";
        public const string Shipping = "Shipping";
        public const string Completed = "Completed";
        public const string Cancelled = "Cancelled";

        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = Pending;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
