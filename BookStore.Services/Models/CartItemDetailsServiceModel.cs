using BookStore.Common.Mapping;
using BookStore.Data.Models;

namespace BookStore.Services.Models
{
    public class CartItemDetailsServiceModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
