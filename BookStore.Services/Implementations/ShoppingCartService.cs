using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Services.Models;
using BookStore.Data;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly BookStoreDbContext db;

        public ShoppingCartService(BookStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CartItemDetailsServiceModel>> Details(IEnumerable<int> itemIds, Dictionary<int, int> itemQuantities)
        {
            var books = await this.db
                   .Books
                   .Where(b => itemIds.Contains(b.Id))
                   .ProjectTo<CartItemDetailsServiceModel>()
                   .ToListAsync();

            foreach (var item in books)
            {
                item.Quantity = itemQuantities[item.Id];
            }

            return books;
        }
    }
}
