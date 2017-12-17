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

        public void AddToCart(int itemId)
        {
            var cartItem = this.cartItems.FirstOrDefault(i => i.Id == itemId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Id = itemId,
                    Quantity = 1
                };

                this.cartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
        }

        public void RemoveFromCart(int itemId)
        {
            var cartItem = this.cartItems
                .Where(i => i.Id == itemId)
                .FirstOrDefault();

            if (cartItem != null)
            {
                this.cartItems.Remove(cartItem);
            }
        }

        public IEnumerable<CartItem> CartItems => new List<CartItem>(this.cartItems);
    }
}
