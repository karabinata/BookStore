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
             string iSBN,
             Category category,
             bool isNew,
             int publicationYear,
             decimal price,
             Condition condition,
             string conditionNote,
             string language,
             string subtitle,
             string seriesAndLibraries,
             string translatorName,
             string paintorName,
             byte[] coverPicture,
             byte[] firstPicture,
             byte[] secondPicture,
             byte[] thirdPicture,
             Coverage coverage,
             string keyWords,
             string format,
             double width,
             double heigth,
             double thickness,
             int weigth,
             string information,
             string notesForTraider,
             string traderId);

        Task<IEnumerable<BookListingServiceModel>> AllAsync(int page = 1, int pageSize = 10);

        Task<BookDetailsServiceModel> DetailsAsync(int id);

        Task<int> TotalAsync();
    }
}
