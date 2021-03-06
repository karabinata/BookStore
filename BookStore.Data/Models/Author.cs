﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Data.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MinLength(FirstNameMinLength)]
        [MaxLength(NamesMaxLength)]
        public string Name { get; set; }

        public List<BookAuthor> Books { get; set; } = new List<BookAuthor>();
    }
}
