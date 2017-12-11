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

        public async Task<IActionResult> All(int page = 1, int pageSize = 5)
        {
            var books = await this.books.AllAsync(page, pageSize);

            var allBooks = new BookListingViewModel
            {
                Books = books,
                TotalBooks = await this.books.TotalAsync(),
                CurrentPage = page,
                PageSize = pageSize
            };

            return View(allBooks);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Order(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var orderResult = await this.orders.OrderBookAsync(userId, id);

            if (!orderResult)
            {
                TempData.AddErrorMessage("За съжаление поръчката не може да се осъществи, артикулът е изчерпан.");
                return RedirectToAction(nameof(Details), new { id });
            }

            TempData.AddSuccessMessage("Поръчката е успешна.");

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Unorder(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var orderResult = await this.orders.UnorderBookAsync(userId, id);

            if (!orderResult)
            {
                TempData.AddErrorMessage("За съжаление поръчката не може да се отмени, възникнала е някаква грешка, моля свържете се с продавача.");
                return RedirectToAction(nameof(Details), new { id });
            }

            TempData.AddSuccessMessage("Поръчката е отменена успешно.");

            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = new BookDetailsViewModel
            {
                Book = await this.books.DetailsAsync(id)
            };

            if (model.Book == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);

                model.IsOrderedByUser = await this.orders
                    .IsOrdered(userId, id);
            }

            return View(model);
        }
    }
}
