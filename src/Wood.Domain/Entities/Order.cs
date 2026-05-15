using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wood.Domain.Common;
using Wood.Domain.Enums;

namespace Wood.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public bool DeliveryRequired { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
