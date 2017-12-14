using BookStore.Services.Books.Models;

namespace BookStore.Web.Areas.Books.Models
{
    public class BookDetailsViewModel
    {
        public BookDetailsServiceModel Book { get; set; }

        public bool IsOrderedByUser { get; set; }

        public bool CanCustomerUnorderBook { get; set; }

        public bool IsThisBookBelongsToTheCurrentUser { get; set; }
    }
}
