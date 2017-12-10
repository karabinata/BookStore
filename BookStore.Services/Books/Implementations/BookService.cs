using System.Threading.Tasks;
using BookStore.Data.Models;
using BookStore.Data;
using BookStore.Services.Books.Models;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using BookStore.Services.Authors;
using BookStore.Services.Publishers;

namespace BookStore.Services.Books.Implementations
{
    public class BookService : IBookService
    {
        private readonly BookStoreDbContext db;
        private readonly IAuthorService authors;
        private readonly IPublisherService publishers;

        public BookService(BookStoreDbContext db, IAuthorService authors, IPublisherService publishers)
        {
            this.db = db;
            this.authors = authors;
            this.publishers = publishers;
        }

        public async Task<IEnumerable<BookListingServiceModel>> AllAsync(int page = 1, int pageSize = 5)
            => await this.db
                .Books
                .OrderByDescending(b => b.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<BookListingServiceModel>()
                .ToListAsync();

        public async Task<int> CreateAsync(
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
            string traderId)
        {
            var authors = authorNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var bookAuthorIds = await this.authors.CreateAsync(authors);

            int? publisherId = null;

            if (!string.IsNullOrEmpty(publisherName))
            {
                publisherId = await this.publishers.CreateAsync(publisherName);
            }

            var book = new Book
            {
                Title = title,
                TraderId = traderId,
                PublisherId = publisherId,
                BooksAvailable = booksAvailable,
                ISBN = iSBN,
                Category = category,
                IsNew = isNew,
                PublicationYear = publicationYear,
                Price = price,
                Condition = condition,
                ConditionNote = conditionNote,
                Language = language,
                Subtitle = subtitle,
                SeriesAndLibraries = seriesAndLibraries,
                TranslatorName = translatorName,
                PaintorName = paintorName,
                CoverPicture = coverPicture,
                FirstPicture = firstPicture,
                SecondPicture = secondPicture,
                ThirdPicture = thirdPicture,
                Coverage = coverage,
                KeyWords = keyWords,
                Format = format,
                Width = width,
                Heigth = heigth,
                Тhickness = thickness,
                Weigth = weigth,
                Information = information,
                NotesForTraider = notesForTraider
            };

            if (publisherId != null)
            {
                var publisher = await this.db
                    .Publishers.FindAsync(publisherId);

                publisher.Books.Add(book);
            }

            foreach (var authorId in bookAuthorIds)
            {
                book.Authors.Add(new BookAuthor
                {
                    AuthorId = authorId,
                    BookId = book.Id
                });
            }

            this.db.Add(book);

            await this.db.SaveChangesAsync();

            return book.Id;
        }

        public async Task<BookDetailsServiceModel> DetailsAsync(int id)
            => await this.db
                .Books
                .Where(b => b.Id == id)
                .ProjectTo<BookDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<int> TotalAsync()
            => await this.db.Books.CountAsync();
    }
}
