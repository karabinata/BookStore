using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Data.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NamesMaxLength)]
        public string Name { get; set; }

        public string Country { get; set; }

        public List<Book> Books { get; set; }
    }
}
