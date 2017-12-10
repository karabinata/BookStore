using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Data.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookStore.Services.Orders.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly BookStoreDbContext db;

        public OrderService(BookStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> OrderBookAsync(string userId, int bookId)
        {
            var book = await this.db.Books.FindAsync(bookId);

            if (book == null)
            {
                return false;
            }

            var order = new Order
            {
                CustomerId = userId,
                OrderDate = DateTime.UtcNow,
                Shipping = 0,
                Subtotal = 0,
                Total = book.Price
            };

            this.db.Add(order);

            var bookOrder = new OrderBook
            {
                BookId = bookId,
                OrderId = order.Id
            };

            this.db.Add(bookOrder);

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

            if (order.OrderDate.AddDays(3) > DateTime.UtcNow)
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
    }
}
