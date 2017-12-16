using BookStore.Services.Books.Models;
using System;
using System.Collections.Generic;

namespace BookStore.Web.Areas.Books.Models
{
    public class BookListingViewModel
    {
        public IEnumerable<BookListingServiceModel> Books { get; set; }

        public int TotalBooks { get; set; }

        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalBooks / this.PageSize);

        public int CurrentPage { get; set; }

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;
    }
}
