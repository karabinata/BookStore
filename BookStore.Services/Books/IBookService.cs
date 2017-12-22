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
             string authorNames,
             string publisherName,
             Category category,
             int publicationYear,
             decimal price,
             Condition condition,
             string language,
             byte[] coverPicture,
             Coverage coverage,
             string description,
             string traderId);

        Task<IEnumerable<BookListingServiceModel>> AllAsync(string orderBy = "Id", string orderDirection = "descending", int page = 1, int pageSize = 5);

        Task<IEnumerable<LastThreeBookServiceModel>> LastThreeBooksAsync();

        Task<IEnumerable<BookListingServiceModel>> BooksByCurrentUserAsync(string userId, int page = 1, int pageSize = 4);

        Task<IEnumerable<BookListingServiceModel>> SearchBookAsync(string searchIn, string searchText = "", string orderBy = "Id",
            string orderDirection = "descending", int page = 1, int pageSize = 5);

        Task<BookDetailsServiceModel> DetailsAsync(int id);

        Task<bool> EditAsync(
             string userId,
             int bookId,
             string title,
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

        Task<string> FindBookTraderAsync(int bookId);

        Task<bool> DeteleteAsync(string userId, int bookId);

        Task<int> TotalAsync();

        Task<int> TotalByUserAsync(string userId);

        Task<bool> ExistsAsync(string userId, int bookId);

        Task<byte[]> GetCoverPicture(int bookId);
    }
}