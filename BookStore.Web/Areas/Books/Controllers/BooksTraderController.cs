using BookStore.Data.Models;
using BookStore.Services.Books;
using BookStore.Services.Books.Models;
using BookStore.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

using static BookStore.Data.DataConstants;

namespace BookStore.Web.Areas.Books.Controllers
{
    public class BooksTraderController : BooksBaseController
    {
        private readonly IBookService books;
        private readonly UserManager<User> userManager;

        public BooksTraderController(IBookService books, UserManager<User> userManager)
        {
            this.books = books;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateServiceModel model, IFormFile coverPicture)
        {
            if (!(coverPicture.FileName.EndsWith(".jpg")
                || coverPicture.FileName.EndsWith(".png")
                || coverPicture.FileName.EndsWith(".gif"))
                || coverPicture.Length > PictureSize)
            {
                ModelState.AddModelError(string.Empty, "Снимката трябва да е с разширение: \".zip\", \".png\" или \".gif\", как и да е с размер до 2 MB.");
                return View(model);
            }

            var pictureContents = await coverPicture.ToByteArrayAsync();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var traderId = this.userManager.GetUserId(User);

            var bookId = await this.books
                .CreateAsync(
                    model.Title,
                    model.BooksAvailable,
                    model.AuthorNames,
                    model.PublisherName,
                    model.ISBN,
                    model.Category,
                    model.IsNew,
                    model.PublicationYear,
                    model.Price,
                    model.Condition,
                    model.ConditionNote,
                    model.Language,
                    model.Subtitle,
                    model.SeriesAndLibraries,
                    model.TranslatorName,
                    model.PaintorName,
                    pictureContents,
                    model.FirstPicture,
                    model.SecondPicture,
                    model.ThirdPicture,
                    model.Coverage,
                    model.KeyWords,
                    model.Format,
                    model.Width,
                    model.Heigth,
                    model.Тhickness,
                    model.Weigth,
                    model.Information,
                    model.NotesForTraider,
                    traderId
                );

            TempData.AddSuccessMessage($"Книгата със заглавие {model.Title} е добавена успешно.");
            return View();
        }
    }
}
