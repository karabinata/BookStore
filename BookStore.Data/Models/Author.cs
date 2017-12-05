using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Data.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MinLength(AuthorFirstNameMinLength)]
        [MaxLength(AuthorFirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(AuthorLastNameMinLength)]
        [MaxLength(AuthorLastNameMaxLength)]
        public string LastName { get; set; }

        public List<BookAuthor> Books { get; set; } = new List<BookAuthor>();
    }
}
