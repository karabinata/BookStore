using System;
using System.Collections.Generic;

namespace BookStore.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Shipping { get; set; }

        public decimal Total { get; set; }

        public string CustomerId { get; set; }

        public User Customer { get; set; }

        public List<OrderBook> Books { get; set; } = new List<OrderBook>();
    }
}
