using BookStore.Services;
using BookStore.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Web.Components
{
    public class ShoppingCartWidget : ViewComponent
    {
        private readonly IShoppingCartManager shoppingCartManager;

        public ShoppingCartWidget(IShoppingCartManager shoppingCartManager)
        {
            this.shoppingCartManager = shoppingCartManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var shoppingCardId = this.HttpContext.Session.GetShoppingCartId();

            var inventory = this.shoppingCartManager.ShoppingCartItemsQuantity(shoppingCardId);

            return View(inventory);
        }
    }
}