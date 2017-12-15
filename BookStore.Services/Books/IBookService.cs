using BookStore.Data.Models;
using BookStore.Services.Books.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services.Books
{
    public interface IBookService
    {
        Task<int> CreateAsync(
             string title,
             int booksAvailable,
             string authorNames,
             string publisherName,
             Category category,
             bool isNew,
             int publicationYear,
             decimal price,
             Condition condition,
             string language,
             byte[] coverPicture,
             Coverage coverage,
             string description,
             string traderId);

        Task<IEnumerable<BookListingServiceModel>> AllAsync(int page = 1, int pageSize = 5);

        Task<IEnumerable<BookListingServiceModel>> BooksByCurrentUserAsync(string userId, int page = 1, int pageSize = 4);

        Task<IEnumerable<BookListingServiceModel>> SearchBookAsync(string searchIn, int page = 1, int pageSize = 5, string searchText = "");

        Task<BookDetailsServiceModel> DetailsAsync(int id);

        Task<bool> EditAsync(
             string userId,
             int bookId,
             string title,
             int booksAvailable,
             string authorNames,
             string publisherName,
             Category category,
             bool isNew,
             int publicationYear,
             decimal price,
             Condition condition,
             string language,
             byte[] coverPicture,
             Coverage coverage,
             string description);

        Task<bool> DeteleteAsync(string userId, int bookId);

        Task<int> TotalAsync();

        Task<bool> ExistsAsync(string userId, int bookId);

        Task<byte[]> GetCoverPicture(int bookId);
    }
}
