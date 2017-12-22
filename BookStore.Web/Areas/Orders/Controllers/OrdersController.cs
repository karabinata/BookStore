using BookStore.Data.Models;
using BookStore.Services.Books;
using BookStore.Services.Orders;
using BookStore.Services.Users.Models;
using BookStore.Web.Areas.Admin.Models.Order;
using BookStore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStore.Web.Areas.Orders.Models;
using BookStore.Services.Users;

namespace BookStore.Web.Areas.Orders.Controllers
{
    [Authorize]
    public class OrdersController : OrdersBaseController
    {
        private readonly IOrderService orders;
        private readonly IBookService books;
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public OrdersController(IOrderService orders, IBookService books, IUserService users, UserManager<User> userManager)
        {
            this.orders = orders;
            this.books = books;
            this.users = users;
            this.userManager = userManager;
        }
        
        public async Task<IActionResult> MyOrders(string orderBy = "Id", string orderDirection = "Descending", int page = 1, int pageSize = 4)
        {
            var userId = this.userManager.GetUserId(User);

            var orders = await this.orders.MyOrdersAsync(userId, orderBy, orderDirection, page, pageSize);

            if (orders == null)
            {
                return NotFound();
            }

            var model = new OrderListingViewModel
            {
                Orders = orders,
                CurrentPage = page,
                PageSize = pageSize,
                TotalOrders = await this.orders.TotalByUserAsync(userId)
            };

            return View(model);
        }

        public async Task<IActionResult> OrdersFromMe(string orderBy = "Id", string orderDirection = "Descending", int page = 1, int pageSize = 4)
        {
            var userId = this.userManager.GetUserId(User);

            var orders = await this.orders.OrdersFomMeAsync(userId, orderBy, orderDirection, page, pageSize);

            if (orders == null)
            {
                return NotFound();
            }

            var model = new OrderListingViewModel
            {
                Orders = orders,
                CurrentPage = page,
                PageSize = pageSize,
                TotalOrders = await this.orders.TotalByUserAsync(userId)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserOrderInfo(UserDetailsServiceModel model)
        {
            var userId = this.userManager.GetUserId(User);

            await this.users.UpdateOrderInfoAsync(userId, model.Address, model.City, model.PhoneNumber);

            return RedirectToAction(nameof(ShoppingCartController.Items), new { area = "", controller = "ShoppingCart" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await this.orders.DetailsAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var userId = this.userManager.GetUserId(User);

            return View(new OrderDetailsViewModel
            {
                Order = order,
                UserInfo = await this.orders.UserInfoAsync(userId)
            });
        }
    }
}
