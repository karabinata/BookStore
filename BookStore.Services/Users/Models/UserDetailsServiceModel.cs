﻿using BookStore.Common.Mapping;
using BookStore.Data.Models;

namespace BookStore.Services.Users.Models
{
    public class UserDetailsServiceModel : IMapFrom<User>
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }
    }
}
