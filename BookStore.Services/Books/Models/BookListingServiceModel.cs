using AutoMapper;
using BookStore.Common.Mapping;
using BookStore.Data.Models;
using System.Linq;

namespace BookStore.Services.Books.Models
{
    public class BookListingServiceModel : IMapFrom<Book>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public byte[] CoverPicture { get; set; }

        public string Title { get; set; }

        public string AuthorNames { get; set; }

        public Category Category { get; set; }

        public decimal Price { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Book, BookListingServiceModel>()
                .ForMember(b => b.AuthorNames, cfg => cfg.MapFrom(b => string.Join(", ", b.Authors.Select(a => a.Author.Name).ToList())));
        }
    }
}
