using BookStore.Data.Models;

namespace BookStore.Services.Books.Models
{
    public class BookCreateServiceModel
    {
        public string Title { get; set; }

        public string AuthorNames { get; set; }
        
        public string PublisherName { get; set; }
        
        public Category Category { get; set; }

        public int PublicationYear { get; set; }

        public decimal Price { get; set; }
        
        public Condition Condition { get; set; }
        
        public string Language { get; set; }
        
        public byte[] CoverPicture { get; set; }

        public Coverage Coverage { get; set; }

        public string Description { get; set; }
    }
}
