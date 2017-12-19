using System.Collections.Generic;
using BookStore.Services.Models;
using System.Collections.Concurrent;
using System.Linq;

namespace BookStore.Services.Implementations
{
    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> carts;

        public ShoppingCartManager()
        {
            this.carts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void AddToCart(string id, int itemId)
        {
            var shoppingCart = this.GetShoppingCart(id);

            shoppingCart.AddToCart(itemId);
        }

        public void DecreaseItemQuantity(string id, int itemId)
        {
            var shoppingCart = this.GetShoppingCart(id);

            shoppingCart.DecreaseQuantity(itemId);
        }

        public IEnumerable<CartItem> GetItems(string id)
        {
            var shoppingCart = this.GetShoppingCart(id);

            return new List<CartItem>(shoppingCart.CartItems);
        }

        public void RemoveFromCart(string id, int itemId)
        {
            var shoppingCart = this.GetShoppingCart(id);

            shoppingCart.RemoveFromCart(itemId);
        }

        public int ShoppingCartItemsQuantity(string id)
        {
            var shoppingCart = this.GetShoppingCart(id);

            return shoppingCart.CartItems.Sum(c => c.Quantity);
        }

        private ShoppingCart GetShoppingCart(string id)
            => this.carts.GetOrAdd(id, new ShoppingCart());

        public void Clear(string id) => this.GetShoppingCart(id).Clear();
    }
}
