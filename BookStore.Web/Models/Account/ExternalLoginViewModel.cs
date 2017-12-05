using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.Models.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
