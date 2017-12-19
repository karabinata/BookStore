﻿using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Data.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BookStore.Services.Orders.Models;
using BookStore.Common.Extensions;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.IO;

namespace BookStore.Services.Orders.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly BookStoreDbContext db;
        private readonly IShoppingCartService shoppingCartService;

        public OrderService(BookStoreDbContext db, IShoppingCartService shoppingCartService)
        {
            this.db = db;
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IEnumerable<OrderListingServiceModel>> AllAsync(string orderBy = "Id", string orderDirection = "descending", int page = 1, int pageSize = 4)
         => await this.db
                .Orders
                .OrderBy<Order>(orderBy, orderDirection)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<OrderListingServiceModel>()
                .ToListAsync();


        public async Task<IEnumerable<OrderListingServiceModel>> MyOrdersAsync(string userId, string orderBy = "Id", string orderDirection = "descending", int page = 1, int pageSize = 4)
            => await this.db
                .Orders
                .Where(o => o.Customer.Id == userId)
                .OrderBy<Order>(orderBy, orderDirection)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<OrderListingServiceModel>()
                .ToListAsync();

        public async Task<OrderDetailsServiceModel> DetailsAsync(int orderId)
        {
            var order = await this.db
                .Orders
                .Where(o => o.Id == orderId)
                .ProjectTo<OrderDetailsServiceModel>()
                .FirstOrDefaultAsync();

            order.BooksIdsAndTitles = await this.db
                .OrderBooks
                .Where(b => b.OrderId == orderId)
                .ProjectTo<BooksInOrder>()
                .ToListAsync();

            return order;
        }

        public async Task<bool> OrderBookAsync(
            string customerId, 
            IEnumerable<int> bookIds, 
            decimal totalPrice,
            Dictionary<int, int> itemQuantities)
        {
            var order = new Order
            {
                CustomerId = customerId,
                TotalPrice = totalPrice,
                Address = "Some adress",
                OrderDate = DateTime.UtcNow,
                Quantity = itemQuantities.Sum(i => i.Value)
            };

            this.db.Add(order);
            await this.db.SaveChangesAsync();

            var itemsWithDetails = await this.shoppingCartService
                    .Details(bookIds, itemQuantities);

            foreach (var item in itemsWithDetails)
            {
                var book = this.db.Books.Find(item.Id);

                if (book != null)
                {
                    order.TraderId = book.TraderId;

                    var orderBook = new OrderBook
                    {
                        OrderId = order.Id,
                        BookId = item.Id,
                        Price = item.Price,
                        Quantity = item.Quantity
                    };

                    order.Books.Add(orderBook);

                    this.db.Add(orderBook);
                }
            }

            await this.db.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> UnorderBookAsync(string userId, int bookId)
        {
            var orderBook = await this.db
                .OrderBooks
                .Where(ob => ob.BookId == bookId)
                .FirstOrDefaultAsync();

            if (orderBook == null)
            {
                return false;
            }

            var order = await this.db
                .Orders
                .Where(o => o.CustomerId == userId && orderBook.BookId == bookId && orderBook.OrderId == o.Id)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return false;
            }

            if (order.OrderDate.AddDays(3) < DateTime.UtcNow)
            {
                return false;
            }

            this.db.Orders.Remove(order);
            this.db.OrderBooks.Remove(orderBook);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsOrdered(string userId, int bookId)
            => await this.db
                .Orders
                .AnyAsync(o => o.CustomerId == userId && o.Books.Any(b => b.BookId == bookId));

        public async Task<int> TotalAsync()
            => await this.db.Orders.CountAsync();
    }
}
