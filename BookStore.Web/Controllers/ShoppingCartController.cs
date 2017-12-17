using BookStore.Services;
using BookStore.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;

        public ShoppingCartController(IShoppingCartManager shoppingCartManager)
        {
            this.shoppingCartManager = shoppingCartManager;
        }

        public IActionResult AddToCart(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetString("Shopping_Cart_Id");

            if (shoppingCartId == null)
            {
                shoppingCartId = Guid.NewGuid().ToString();
                this.HttpContext.Session.SetString("Shopping_Cart_Id", shoppingCartId);
            }

            this.shoppingCartManager.AddToCart(shoppingCartId, new CartItem
            {
                BookId = id
            });

            return View();
        }
    }
}
