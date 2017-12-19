using BookStore.Services.Models;
using System.Collections.Generic;

namespace BookStore.Services
{
    public interface IShoppingCartManager
    {
        void AddToCart(string id, int itemId);

        int ShoppingCartItemsQuantity(string id);

        void DecreaseItemQuantity(string id, int itemId);

        void RemoveFromCart(string id, int itemId);

        IEnumerable<CartItem> GetItems(string id);

        void Clear(string id);
    }
}
