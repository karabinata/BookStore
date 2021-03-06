﻿using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Името е задължително.")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Презиме")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Фамилията е задължителна.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Потребителското име е задължително.")]
        [StringLength(UsernameMaxLength, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа дълга.", MinimumLength = UsernameMinLength)]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; }

        [Required(ErrorMessage = "И-мейлът е задължителен.")]
        [EmailAddress]
        [Display(Name = "И-мейл")]
        public string Email { get; set; }
        
        [Phone]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [StringLength(PasswordMaxLength, ErrorMessage = "{0}та трябва да бъде между {2} и {1} символа дълга.", MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повтори парола")]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
        public string ConfirmPassword { get; set; }
    }
}
