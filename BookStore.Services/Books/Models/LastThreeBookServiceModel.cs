using BookStore.Common.Mapping;
using BookStore.Data.Models;

namespace BookStore.Services.Books.Models
{
    public class LastThreeBookServiceModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public byte[] CoverPicture { get; set; }

        public string Title { get; set; }
    }
}
