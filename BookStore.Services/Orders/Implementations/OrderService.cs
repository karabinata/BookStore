using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Data.Models;
using System;

namespace BookStore.Services.Orders.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly BookStoreDbContext db;

        public OrderService(BookStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> OrderItemAsync(string userId, int bookId)
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
    }
}
