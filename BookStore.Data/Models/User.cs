using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]
        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
