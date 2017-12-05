﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
