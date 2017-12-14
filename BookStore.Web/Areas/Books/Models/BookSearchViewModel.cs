using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.Areas.Books.Models
{
    public class BookSearchViewModel : BookListingViewModel
    {
        [FromQuery]
        public string SearchText { get; set; }

        [FromQuery]
        public string SearchIn { get; set; }
    }
}
