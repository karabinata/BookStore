using System;
using System.Collections.Generic;

namespace BookStore.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Price { get; set; }

        public string TraderId { get; set; }

        public User Trader { get; set; }

        public string CustomerId { get; set; }

        public User Customer { get; set; }

        public List<OrderBook> Books { get; set; } = new List<OrderBook>();
    }
}
