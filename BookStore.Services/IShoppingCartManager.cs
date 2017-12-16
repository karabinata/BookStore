using BookStore.Services.Models;
using System.Collections.Generic;

namespace BookStore.Services
{
    public interface IShoppingCartManager
    {
        void AddToCart(string id, CartItem cartItem);

        void RemoveFromCart(string id, int itemId);

        IEnumerable<CartItem> GetItems(string id);
    }
}
