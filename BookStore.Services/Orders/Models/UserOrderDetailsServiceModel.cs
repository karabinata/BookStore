using BookStore.Common.Mapping;
using BookStore.Data.Models;

namespace BookStore.Services.Orders.Models
{
    public class UserOrderDetailsServiceModel : IMapFrom<User>
    {
        public string City { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
