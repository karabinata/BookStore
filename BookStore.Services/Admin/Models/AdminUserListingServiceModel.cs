using BookStore.Common.Mapping;
using BookStore.Data.Models;

namespace BookStore.Services.Admin.Models
{
    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
