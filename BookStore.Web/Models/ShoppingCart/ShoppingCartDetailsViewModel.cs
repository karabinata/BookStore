using BookStore.Services.Models;
using BookStore.Services.Users.Models;
using System.Collections.Generic;

namespace BookStore.Web.Models.ShoppingCart
{
    public class ShoppingCartDetailsViewModel
    {
        public IEnumerable<CartItemDetailsServiceModel> Items { get; set; }

        public UserDetailsServiceModel Customer { get; set; }
    }
}
