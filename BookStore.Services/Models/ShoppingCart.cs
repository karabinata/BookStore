using System.Collections.Generic;
using System.Linq;

namespace BookStore.Services.Models
{
    public class ShoppingCart
    {
        private readonly IList<CartItem> cartItems;

        public ShoppingCart()
        {
            this.cartItems = new List<CartItem>();
        }

        public void AddToCart(CartItem item)
        {
            if (item != null)
            {
                this.cartItems.Add(item);
            }
        }

        public void RemoveFromCart(int itemId)
        {
            var cartItem = this.cartItems
                .Where(i => i.BookId == itemId)
                .FirstOrDefault();

            if (cartItem != null)
            {
                this.cartItems.Remove(cartItem);
            }
        }

        public IEnumerable<CartItem> CartItems => new List<CartItem>(this.cartItems);
    }
}
