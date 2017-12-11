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

        [MaxLength(ISBNMaxLength)]
        public string ISBN { get; set; }

        [Required]
        public Category Category { get; set; }

        public bool IsNew { get; set; }

        public int PublicationYear { get; set; }

        public decimal Price { get; set; }

        [Required]
        public Condition Condition { get; set; }

        [MaxLength(ConditionNoteMaxLength)]
        public string ConditionNote { get; set; }

        [Required]
        [MaxLength(BookLanguageMaxLength)]
        public string Language { get; set; }

        [MaxLength(BookTitleMaxLength)]
        public string Subtitle { get; set; }

        public string SeriesAndLibraries { get; set; }

        [MaxLength(NamesMaxLength)]
        public string TranslatorName { get; set; }

        [MaxLength(NamesMaxLength)]
        public string PaintorName { get; set; }

        [MaxLength(PictureSize)]
        public byte[] CoverPicture { get; set; }

        [MaxLength(PictureSize)]
        public byte[] FirstPicture { get; set; }

        [MaxLength(PictureSize)]
        public byte[] SecondPicture { get; set; }

        [MaxLength(PictureSize)]
        public byte[] ThirdPicture { get; set; }

        public Coverage Coverage { get; set; }

        public string KeyWords { get; set; }

        public string Format { get; set; }

        public double? Width { get; set; }

        public double? Heigth { get; set; }

        public double? Тhickness { get; set; }

        public int? Weigth { get; set; }

        public string Information { get; set; }

        public string NotesForTraider { get; set; }

        public string Description { get; set; }

        public List<BookAuthor> Authors { get; set; } = new List<BookAuthor>();

        public int? PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public List<OrderBook> Orders { get; set; } = new List<OrderBook>();
    }
}
