using BookStore.Services.Orders.Models;

namespace BookStore.Web.Areas.Orders.Models
{
    public class OrderDetailsViewModel
    {
        public OrderDetailsServiceModel Order { get; set; }

        public UserOrderDetailsServiceModel UserInfo { get; set; }
    }
}
