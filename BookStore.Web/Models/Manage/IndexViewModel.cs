using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Web.Models.Manage
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        [Required]
        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]
        public string LastName { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
