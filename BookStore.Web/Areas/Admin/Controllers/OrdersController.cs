using BookStore.Services.Orders;
using BookStore.Web.Areas.Admin.Models.Order;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class OrdersController : BaseAdminController
    {
        private readonly IOrderService orders;

        public OrdersController(IOrderService orders)
        {
            this.orders = orders;
        }

        public async Task<IActionResult> All(string orderBy = "Id", string orderDirection = "descending", int page = 1, int pageSize = 4)
        {
            var model = new OrderListingViewModel
            {
                Orders = await this.orders.AllAsync(orderBy, orderDirection, page, pageSize),
                TotalOrders = await this.orders.TotalAsync(),
                CurrentPage = page,
                PageSize = pageSize
            };

            return View(model);
        }
    }
}
