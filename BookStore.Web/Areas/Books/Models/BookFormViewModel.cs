using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Web.Areas.Books.Models
{
    public class BookFormViewModel
    {
        [Required]
        [MinLength(BookTitleMinLength)]
        [MaxLength(BookTitleMaxLength)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Налични бройки")]
        public int BooksAvailable { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public List<SelectListItem> Category { get; set; } = new List<SelectListItem>();
        
        public bool IsNew { get; set; }

        [Display(Name = "Година на издаване")]
        public int PublicationYear { get; set; }

        [Display(Name = "Брой страници")]
        public int? NumberOfPages { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Състояние")]
        public List<SelectListItem> Condition { get; set; }

        [MaxLength(ConditionNoteMaxLength)]
        [Display(Name = "Коментар за състоянието")]
        public string ConditionNote { get; set; }

        [Required]
        [MaxLength(BookLanguageMaxLength)]
        [Display(Name = "Език")]
        public string Language { get; set; }

        [MaxLength(PictureSize)]
        [Display(Name = "Снимка на артикула")]
        public byte[] CoverPicture { get; set; }

        [Display(Name = "Тип корици")]
        public List<SelectListItem> Coverage { get; set; }

        [MaxLength(NamesMaxLength)]
        [Display(Name = "Автор(и)")]
        public string AuthorNames { get; set; }

        [MaxLength(NamesMaxLength)]
        [Display(Name = "Издател")]
        public string PublisherName { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
