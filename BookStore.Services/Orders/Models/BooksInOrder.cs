using AutoMapper;
using BookStore.Common.Mapping;
using BookStore.Data.Models;

namespace BookStore.Services.Orders.Models
{
    public class BooksInOrder : IMapFrom<Book>, IHaveCustomMapping
    {
        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Book, BooksInOrder>()
                .ForMember(b => b.BookId, cfg => cfg.MapFrom(b => b.Id))
                .ForMember(b => b.BookTitle, cfg => cfg.MapFrom(b => b.Title));
        }
    }
}
