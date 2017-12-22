using System.Diagnostics;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using BookStore.Services.Books;
using System.Threading.Tasks;

namespace BookStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private IBookService books;

        public HomeController(IBookService books)
        {
            this.books = books;
        }

        public async Task<IActionResult> Index()
        {
            var lastBooks = await this.books.LastThreeBooksAsync();

            return View(lastBooks);
        }

        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
