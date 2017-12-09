using BookStore.Services.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BookStore.Web.Areas.Admin.Models.Users
{
    public class UserViewModel
    {
        public IEnumerable<SelectListItem> Roles { get; set; }

        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }
    }
}
