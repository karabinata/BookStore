using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

using static BookStore.Data.DataConstants;

namespace BookStore.Data.Models
{
    public class Trader : User
    {
        [Required]
        [MinLength(FirmNameMinLength)]
        [MaxLength(FirmNameMaxLength)]
        public string FirmName { get; set; }

        [Required]
        [MaxLength(UicMaxLength)]
        public string UIC { get; set; }

        public bool VatRegistration { get; set; }

        [Required]
        [MinLength(ResponsiblePersonMinLength)]
        [MaxLength(ResponsiblePersonMaxLength)]
        public string ResponsiblePerson { get; set; }

        public City City { get; set; }
        
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        public bool InvoiceWanted { get; set; }
    }
}
