using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Web.Areas.Books.Models
{
    public class BookFormViewModel
    {
        [Required(ErrorMessage = "Заглавието е задължително")]
        [MinLength(BookTitleMinLength)]
        [MaxLength(BookTitleMaxLength)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Категорията е задължителна")]
        [Display(Name = "Категория")]
        public List<SelectListItem> Category { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessage = "Годината на издаване е задължителна")]
        [Display(Name = "Година на издаване")]
        public int PublicationYear { get; set; }

        [Display(Name = "Брой страници")]
        public int? NumberOfPages { get; set; }

        [Required(ErrorMessage = "Цената е задължителна")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Състоянието е задължително")]
        [Display(Name = "Състояние")]
        public List<SelectListItem> Condition { get; set; }

        [StringLength(ConditionNoteMaxLength, ErrorMessage = "{0} трябва да не бъде по-дълъг от {1} символа.")]
        [Display(Name = "Коментар за състоянието")]
        public string ConditionNote { get; set; }

        [Required(ErrorMessage = "Езикът на книгата е задължителен")]
        [StringLength(BookLanguageMaxLength, ErrorMessage = "{0}ът трябва да не бъде по-дълъг от {1} символа.")]
        [Display(Name = "Език")]
        public string Language { get; set; }

        [MaxLength(PictureSize)]
        [Display(Name = "Снимка на артикула")]
        public byte[] CoverPicture { get; set; }

        [Required(ErrorMessage = "Типът корици на книгата е задължителен")]
        [Display(Name = "Тип корици")]
        public List<SelectListItem> Coverage { get; set; }

        [StringLength(NamesMaxLength, ErrorMessage = "Името/имената на авторите не трябва да надвишават {1} на брой символа.")]
        [Display(Name = "Автор(и)")]
        public string AuthorNames { get; set; }

        [StringLength(NamesMaxLength, ErrorMessage = "Името на издателя не трябва да надвишава {1} на брой символа.")]
        [Display(Name = "Издател")]
        public string PublisherName { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
