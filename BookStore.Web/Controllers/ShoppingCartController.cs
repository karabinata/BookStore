using BookStore.Data.Models;
using BookStore.Services;
using BookStore.Services.Orders;
using BookStore.Web.Areas.Books.Controllers;
using BookStore.Web.Infrastructure.Extensions;
using BookStore.Web.Models.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IOrderService orders;
        private readonly UserManager<User> userManager;
        private readonly BookStore.Services.Users.IUserService users;

        public ShoppingCartController(
            IShoppingCartManager shoppingCartManager,
            IShoppingCartService shoppingCartService,
            IOrderService orders,
            UserManager<User> userManager,
            BookStore.Services.Users.IUserService users)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.shoppingCartService = shoppingCartService;
            this.orders = orders;
            this.userManager = userManager;
            this.users = users;
        }

        public async Task<IActionResult> Items()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            var itemQuantities = items.ToDictionary(i => i.Id, i => i.Quantity);

            var itemIds = items.Select(i => i.Id);

            var itemsWithDetails = await this.shoppingCartService
                    .Details(itemIds, itemQuantities);

            var userId = this.userManager.GetUserId(User);

            return View(new ShoppingCartDetailsViewModel
            {
                Items = itemsWithDetails,
                Customer = await this.users.Details(userId)
            });
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            this.Add(id);

            return RedirectToAction(nameof(ItemsController.All), new { area = "Books", controller = "Items" });
        }

        public async Task<IActionResult> IncreaseItemQuantity(int id)
        {
            this.Add(id);

            return RedirectToAction(nameof(Items));
        }

        public async Task<IActionResult> DecreaseItemQuantity(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartManager.DecreaseItemQuantity(shoppingCartId, id);

            TempData.AddSuccessMessage("Броят на артикулите в количката е променен успешно.");

            return RedirectToAction(nameof(Items));
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            TempData.AddSuccessMessage("Артикулът успешно е премахнат от количката.");

            this.shoppingCartManager.RemoveFromCart(shoppingCartId, id);

            return RedirectToAction(nameof(Items));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> FinishOrder()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            var itemIds = items.Select(i => i.Id);

            var itemQuantities = items.ToDictionary(i => i.Id, i => i.Quantity);

            var itemsWithDetails = await this.shoppingCartService
                    .Details(itemIds, itemQuantities);

            var customerId = this.userManager.GetUserId(User);
            var totalPrice = itemsWithDetails.Sum(i => i.Price * i.Quantity);

            var isOrdered = await this.orders.OrderBookAsync(customerId, itemIds, totalPrice, itemQuantities);

            if (!isOrdered)
            {
                return BadRequest();
            }

            this.shoppingCartManager.Clear(shoppingCartId);

            TempData.AddSuccessMessage("Поръчката е направена успешно.");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private void Add(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartManager.AddToCart(shoppingCartId, id);

            TempData.AddSuccessMessage("Артикулът е успешно добавен в количката.");
        }
    }
}
