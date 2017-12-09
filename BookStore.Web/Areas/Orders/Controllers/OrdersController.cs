using BookStore.Data.Models;
using BookStore.Services.Orders;
using BookStore.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Web.Areas.Orders.Controllers
{
    public class OrdersController : OrdersBaseController
    {
        private readonly IOrderService orders;
        private readonly UserManager<User> userManager;

        public OrdersController(IOrderService orders, UserManager<User> userManager)
        {
            this.orders = orders;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Order(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var orderResult = await this.orders.OrderItemAsync(userId, id);

            if (!orderResult)
            {
                TempData.AddErrorMessage("За съжаление поръчката не може да се осъществи, артикулът е изчерпан.");
            }

            TempData.AddSuccessMessage("Поръчката е успешна.");

            return RedirectToAction("Details", "Items", new { id = id, Area = nameof(Books)});
        }
    }
}
