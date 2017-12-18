using BookStore.Data.Models;
using BookStore.Services.Books;
using BookStore.Services.Orders;
using BookStore.Web.Areas.Books.Models;
using BookStore.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

using static BookStore.Data.DataConstants;

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

        public async Task<IActionResult> All(string orderBy = "Id", string orderDirection = "descending", int page = 1, int pageSize = 4)
        {
            var books = await this.books.AllAsync(orderBy, orderDirection, page, pageSize);

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
        public async Task<IActionResult> MyBooks(int page = 1, int pageSize = 4)
        {
            var userId = this.userManager.GetUserId(User);

            var myBooks = new BookListingViewModel
            {
                Books = await this.books.BooksByCurrentUserAsync(userId, page, pageSize),
                TotalBooks = await this.books.TotalAsync(),
                CurrentPage = page,
                PageSize = pageSize
            };

            return View(myBooks);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var bookExist = await this.books.ExistsAsync(this.userManager.GetUserId(User), id);

            if (!bookExist)
            {
                return NotFound();
            }

            var book = await this.books.DetailsAsync(id);

            return View(new BookDetailsViewModel
            {
                Book = book
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookDetailsViewModel model, IFormFile coverPicture)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            byte[] pictureContents = null;

            if (coverPicture != null)
            {
                if (!(coverPicture.FileName.EndsWith(".jpg")
                || coverPicture.FileName.EndsWith(".png")
                || coverPicture.FileName.EndsWith(".gif"))
                || coverPicture.Length > PictureSize)
                {
                    ModelState.AddModelError(string.Empty, "Снимката трябва да е с разширение: \".zip\", \".png\" или \".gif\", как и да е с размер до 2 MB.");
                    return View(model);
                }

                pictureContents = await coverPicture.ToByteArrayAsync();
            }

            else
            {
                pictureContents = await this.books.GetCoverPicture(id);
            }

            var updated = await this.books
                .EditAsync(
                this.userManager.GetUserId(User),
                id,
                model.Book.Title,
                model.Book.BooksAvailable,
                model.Book.AuthorNames,
                model.Book.Publisher,
                model.Book.Category,
                model.Book.IsNew,
                model.Book.PublicationYear,
                model.Book.Price,
                model.Book.Condition,
                model.Book.Language,
                pictureContents,
                model.Book.Coverage,
                model.Book.Description);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.userManager.GetUserId(User);
            var isDeleted = await this.books.DeteleteAsync(userId, id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(MyBooks));
        }

        public async Task<IActionResult> Search(BookSearchViewModel model)
        {
            var searchText = string.IsNullOrEmpty(model.SearchText) ? string.Empty : model.SearchText;

            var viewModel = new BookSearchViewModel
            {
                Books = await this.books.SearchBookAsync(model.SearchIn, model.CurrentPage, model.PageSize, searchText),
                SearchText = searchText,
                PageSize = model.PageSize,
                CurrentPage = model.CurrentPage
            };

            return View(viewModel);
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> Order(int id)
        //{
        //    var customerId = this.userManager.GetUserId(User);
        //    var traderId = await this.books.FindBookTraderAsync(id);

        //    if (traderId == null)
        //    {
        //        return NotFound();
        //    }

        //    var orderResult = await this.orders.OrderBookAsync(traderId, customerId, id);

        //    if (!orderResult)
        //    {
        //        TempData.AddErrorMessage("За съжаление поръчката не може да се осъществи, артикулът е изчерпан.");
        //        return RedirectToAction(nameof(Details), new { id });
        //    }

        //    TempData.AddSuccessMessage("Поръчката е успешна.");

        //    return RedirectToAction(nameof(Details), new { id });
        //}

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

                model.IsThisBookBelongsToTheCurrentUser = userId == model.Book.TraderId;
            }

            return View(model);
        }
    }
}
