using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Web.Models.Account
{
    public class ExternalLoginViewModel
    {
        [Required(ErrorMessage = "И-мейлът е задължителен.")]
        [EmailAddress]
        [Display(Name = "И-мейл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Потребителското име е задължително.")]
        [StringLength(UsernameMaxLength, ErrorMessage = "{0} трябва да бъде между {2} и {1} символа дълга.", MinimumLength = UsernameMinLength)]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Името е задължително.")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилията е задължителна.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        
    }
}
