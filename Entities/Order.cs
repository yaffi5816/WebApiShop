using System;
using System.Collections.Generic;

namespace Entities
{

    public partial class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public bool CurrentStatus { get; set; }

        public DateOnly? OrderDate { get; set; }

        public double OrdersSum { get; set; }

        public string? OriginalSchema { get; set; }

        public string? GeneratedCode { get; set; }

        public virtual ICollection<OrdersItem> OrderItems { get; set; } = new List<OrdersItem>();

        public virtual User User { get; set; } = null!;
    }
}
