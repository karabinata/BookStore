using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        public decimal TotalPrice { get; set; }

        public int Quantity { get; set; }

        public string TraderId { get; set; }

        public User Trader { get; set; }

        public string CustomerId { get; set; }

        public User Customer { get; set; }

        public List<OrderBook> Books { get; set; } = new List<OrderBook>();
    }
}
