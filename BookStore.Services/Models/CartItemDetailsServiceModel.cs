using AutoMapper;
using BookStore.Common.Mapping;
using BookStore.Data.Models;

namespace BookStore.Services.Models
{
    public class CartItemDetailsServiceModel : IMapFrom<Book>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public byte[] CoverPicture { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<CartItem, CartItemDetailsServiceModel>()
                .ForMember(i => i.Quantity, cfg => cfg.MapFrom(c => c.Quantity));
    }
}
