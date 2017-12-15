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

        const string SearchInAuthors = "Author";
        const string SearchInTitles = "Title";

        public BookService(BookStoreDbContext db, IAuthorService authors, IPublisherService publishers)
        {
            this.db = db;
            this.authors = authors;
            this.publishers = publishers;
        }

        public async Task<IEnumerable<BookListingServiceModel>> AllAsync(int page = 1, int pageSize = 4)
            => await this.db
                .Books
                .OrderByDescending(b => b.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<BookListingServiceModel>()
                .ToListAsync();



        public async Task<IEnumerable<BookListingServiceModel>> BooksByCurrentUserAsync(string userId, int page = 1, int pageSize = 4)
            => await this.db
                .Books
                .Where(b => b.TraderId == userId)
                .OrderByDescending(b => b.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<BookListingServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<BookListingServiceModel>> SearchBookAsync(string searchIn, int page = 1, int pageSize = 4, string searchText = "")
        {
            if (searchIn == SearchInAuthors)
            {
                return await this.db
                    .Books
                    .Where(b => b.Authors.Where(
                            a => a.Author.Name.ToLower().Contains(searchText.ToLower())) != null)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<BookListingServiceModel>()
                    .ToListAsync();
            }

            if (searchIn == SearchInTitles)
            {
                return await this.db
                .Books
                .OrderByDescending(b => b.Id)
                .Where(b => b.Title.ToLower().Contains(searchText.ToLower()))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<BookListingServiceModel>()
                .ToListAsync();
            }

            return await this.AllAsync(page, pageSize);
        }

        public async Task<int> CreateAsync(
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
                Category = category,
                IsNew = isNew,
                PublicationYear = publicationYear,
                Price = price,
                Condition = condition,
                Language = language,
                CoverPicture = coverPicture,
                Coverage = coverage,
                Description = description
            };

            if (publisherId != null)
            {
                var publisher = await this.db
                    .Publishers
                    .FindAsync(publisherId);

                publisher.Books.Add(book);

                book.Publisher = await this.db
                .Publishers
                .FindAsync(publisherId);
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


        public async Task<bool> EditAsync(
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
            string description)
        {
            var book = await this.db
                .Books
                .Where(b => b.TraderId == userId && b.Id == bookId)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return false;
            }

            if (book.Publisher != null && book.Publisher.Name != publisherName)
            {
                int? publisherId = null;

                if (!string.IsNullOrEmpty(publisherName))
                {
                    publisherId = await this.publishers.CreateAsync(publisherName);
                }

                if (publisherId != null)
                {
                    var publisher = await this.db
                        .Publishers.FindAsync(publisherId);

                    publisher.Books.Add(book);
                }

                book.PublisherId = publisherId;
            }

            book.Title = title;
            book.BooksAvailable = booksAvailable;
            book.Category = category;
            book.IsNew = isNew;
            book.PublicationYear = publicationYear;
            book.Price = price;
            book.Condition = condition;
            book.Language = language;
            book.CoverPicture = coverPicture;
            book.Coverage = coverage;
            book.Description = description;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeteleteAsync(string userId, int bookId)
        {
            var book = await this.db
                .Books
                .Where(b => b.TraderId == userId && b.Id == bookId)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return false;
            }

            this.db.Books.Remove(book);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<int> TotalAsync()
            => await this.db.Books.CountAsync();

        public async Task<bool> ExistsAsync(string userId, int bookId)
            => await this.db
                .Books
                .AnyAsync(b => b.TraderId == userId && b.Id == bookId);

        public async Task<byte[]> GetCoverPicture(int bookId)
        {
            var book = await this.db.Books.FindAsync(bookId);

            if (book == null)
            {
                return null;
            }

            return book.CoverPicture;
        }
    }
}
