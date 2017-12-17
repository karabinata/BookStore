using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Data.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BookStore.Services.Orders.Models;
using BookStore.Common.Extensions;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;

namespace BookStore.Services.Orders.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly BookStoreDbContext db;

        public OrderService(BookStoreDbContext db)
        {
            this.db = db;
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
                .Books
                .Where(b => b.Orders.All(o => o.OrderId == orderId))
                .ProjectTo<BooksInOrder>()
                .ToListAsync();

            return order;
        }

        public async Task<bool> OrderBookAsync(string traderId, string customerId, int bookId)
        {
            var book = await this.db.Books.FindAsync(bookId);

            if (book == null)
            {
                return false;
            }

            if (!await this.CheckIsBookAvailableForOrder(bookId))
            {
                return false;
            }

            var order = new Order
            {
                TraderId = traderId,
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = book.Price
            };

            this.db.Add(order);

            var bookOrder = new OrderBook
            {
                BookId = bookId,
                OrderId = order.Id
            };

            this.db.Add(bookOrder);

            this.BooksAvailableCount(bookId, -1);

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

            this.BooksAvailableCount(orderBook.BookId, 1);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsOrdered(string userId, int bookId)
            => await this.db
                .Orders
                .AnyAsync(o => o.CustomerId == userId && o.Books.Any(b => b.BookId == bookId));

        private void BooksAvailableCount(int bookId, int count)
        {
            var booksAvailable = this.db
                .Books
                .Find(bookId);

            booksAvailable.BooksAvailable += count;
        }

        private async Task<bool> CheckIsBookAvailableForOrder(int bookId)
        {
            var booksAvailable = this.db
                .Books
                .Find(bookId);

            return booksAvailable.BooksAvailable > 0;
        }

        public async Task<int> TotalAsync()
            => await this.db.Orders.CountAsync();
    }
}
