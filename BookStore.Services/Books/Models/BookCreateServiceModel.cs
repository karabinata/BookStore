using BookStore.Data.Models;
using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Services.Books.Models
{
    public class BookCreateServiceModel
    {
        [Required]
        [MinLength(BookTitleMinLength)]
        [MaxLength(BookTitleMaxLength)]
        public string Title { get; set; }

        public int BooksAvailable { get; set; }

        public string AuthorNames { get; set; }

        [MaxLength(NamesMaxLength)]
        public string PublisherName { get; set; }

        [Required]
        public Category Category { get; set; }

        public bool IsNew { get; set; }

        public int PublicationYear { get; set; }

        public decimal Price { get; set; }

        [Required]
        public Condition Condition { get; set; }

        [Required]
        [MaxLength(BookLanguageMaxLength)]
        public string Language { get; set; }

        [MaxLength(PictureSize)]
        public byte[] CoverPicture { get; set; }

        public Coverage Coverage { get; set; }

        public string Description { get; set; }
    }
}
