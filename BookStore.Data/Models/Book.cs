using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string TraderId { get; set; }

        public User Trader { get; set; }

        [Required]
        [MinLength(BookTitleMinLength)]
        [MaxLength(BookTitleMaxLength)]
        public string Title { get; set; }

        public int BooksAvailable { get; set; }

        [Required]
        public Category Category { get; set; }

        public bool IsNew { get; set; }

        public int PublicationYear { get; set; }

        public int? NumberOfPages { get; set; }

        public decimal Price { get; set; }

        [Required]
        public Condition Condition { get; set; }

        [MaxLength(ConditionNoteMaxLength)]
        public string ConditionNote { get; set; }

        [Required]
        [MaxLength(BookLanguageMaxLength)]
        public string Language { get; set; }

        [MaxLength(PictureSize)]
        public byte[] CoverPicture { get; set; }

        public Coverage Coverage { get; set; }

        public string Description { get; set; }

        public List<BookAuthor> Authors { get; set; } = new List<BookAuthor>();

        public int? PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public List<OrderBook> Orders { get; set; } = new List<OrderBook>();
    }
}
