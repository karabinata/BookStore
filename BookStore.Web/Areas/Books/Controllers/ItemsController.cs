using BookStore.Data.Models;
using BookStore.Services.Books;
using BookStore.Services.Orders;
using BookStore.Web.Areas.Books.Models;
using BookStore.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Areas.Books.Controllers
{
    public class ItemsController : BooksBaseController
    {
        private readonly IBookService books;
        private readonly IOrderService orders;
        private readonly UserManager<User> userManager;

        public ItemsController(IBookService books, IOrderService orders, UserManager<User> userManager)
        {
            this.books = books;
            this.orders = orders;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var books = await this.books.AllAsync();

            var allBooks = new BookListingViewModel
            {
                Books = books,
                CurrentPage = page,
                TotalBooks = await this.books.TotalAsync()
            };

            return View(allBooks);
        }

        [Authorize]
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

            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Details(int id)
            => View(await this.books.DetailsAsync(id));
    }
}
