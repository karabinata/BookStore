using BookStore.Data.Models;
using BookStore.Services.Books;
using BookStore.Services.Orders;
using BookStore.Web.Areas.Admin.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Web.Areas.Orders.Controllers
{
    [Authorize]
    public class OrdersController : OrdersBaseController
    {
        private readonly IOrderService orders;
        private readonly IBookService books;
        private readonly UserManager<User> userManager;

        public OrdersController(IOrderService orders, IBookService books, UserManager<User> userManager)
        {
            this.orders = orders;
            this.books = books;
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
                TotalOrders = await this.orders.TotalAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await this.orders.DetailsAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
