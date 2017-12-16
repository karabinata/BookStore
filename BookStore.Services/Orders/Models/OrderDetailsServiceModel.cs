using AutoMapper;
using BookStore.Common.Mapping;
using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Services.Orders.Models
{
    public class OrderDetailsServiceModel : IMapFrom<Order>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Price { get; set; }

        public string Customer { get; set; }

        public string Trader { get; set; }

        public IEnumerable<BooksInOrder> BooksIdsAndTitles { get; set; } = new List<BooksInOrder>();

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Order, OrderDetailsServiceModel>()
                .ForMember(o => o.Customer, cfg => cfg.MapFrom(o => o.Customer.UserName))
                .ForMember(o => o.Trader, cfg => cfg.MapFrom(o => o.Trader.UserName));
        }
    }
}
