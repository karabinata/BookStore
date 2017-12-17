using BookStore.Services;
using BookStore.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartManager shoppingCartManager, IShoppingCartService shoppingCartService)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Items()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            var itemQuantities = items.ToDictionary(i => i.Id, i => i.Quantity);

            var itemIds = items.Select(i => i.Id);

            var itemsWithDetails = await this.shoppingCartService
                    .Details(itemIds);

            foreach (var item in itemsWithDetails)
            {
                item.Quantity = itemQuantities[item.Id];
            }

            return View("_Items", itemsWithDetails);
        }

        public IActionResult AddToCart(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartManager.AddToCart(shoppingCartId, id);

            return RedirectToAction(nameof(Items));
        }

        [Authorize]
        [HttpPost]
        public IActionResult FinishOrder()
        {
            return null;
        }
    }
}
