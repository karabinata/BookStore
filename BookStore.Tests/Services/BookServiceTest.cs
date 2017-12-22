using BookStore.Data;
using BookStore.Data.Models;
using BookStore.Services.Authors.Implementations;
using BookStore.Services.Books.Implementations;
using BookStore.Services.Publishers.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Tests.Services
{
    public class BookServiceTest
    {
        private const string UserId = "ivan";
        const int BookId = 3;

        public BookServiceTest()
        {
            TestStartUp.Initialize();
        }

        [Fact]
        public async Task AllAsyncOrderByIdAndOrderDirectionDescendingShouldReturnCorrectResult()
        {
            // Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);
            
            var bookService = new BookService(db, authorService, publisherService);

            // Act
            var result = await bookService.AllAsync("Id", "Descending");

            //Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 3
                && r.ElementAt(1).Id == 2
                && r.ElementAt(2).Id == 1)
                .And
                .HaveCount(3);
        }

        [Fact]
        public async Task BooksByCurrentUserAsyncShouldReturnBooksBelongsToTheGivenUserWithValidUserIdOrderedByIdDescending()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.BooksByCurrentUserAsync(UserId);

            //Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 3
                && r.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task SearchBookAsyncShouldReturnCorrectResultForSearchInTitlesWithGivenSearchText()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.SearchBookAsync("Title", "first");

            //Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 1)
                .And
                .HaveCount(1);
        }

        [Fact]
        public async Task CreateAsyncShouldSaveBookSuccessfullyInDb()
        {
            //Arrange
            var db = await this.GetDatabase();

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.CreateAsync
                ("Title", "Some Author", "Publisher", Category.История, 1987, 10, Condition.Отлично, "Български", null, Coverage.Твърди, null, UserId);

            var savedEntry = db.Books.Where(b => b.Title == "Title");

            //Assert
            result
                .Should()
                .Be(1);

            savedEntry
                .Should()
                .NotBeNull();
        }

        [Fact]
        public async Task DetailsAsyncShouldReturnInfoForTheCorrectBook()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.DetailsAsync(BookId);

            //Assert
            result
                .Id.ShouldBeEquivalentTo(BookId);
        }

        [Fact]
        public async Task EditAsyncShouldEditBookSuccessfullyIfBookIsNotNull()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.EditAsync
                (UserId, BookId, "Title", "Some Author", "Publisher", Category.История, false, 1987, 10, Condition.Отлично, "Български", null, Coverage.Твърди, null);

            var editedBook = db.Books.Find(BookId);

            //Assert
            result
                .Should()
                .BeTrue();

            editedBook
                .Title
                .ShouldBeEquivalentTo("Title");
        }

        [Fact]
        public async Task EditAsyncShouldFiledIfBookIsNull()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.EditAsync
                ("sa", BookId, "Title", "Some Author", "Publisher", Category.История, false, 1987, 10, Condition.Отлично, "Български", null, Coverage.Твърди, null);
            
            //Assert
            result
                .Should()
                .BeFalse();
        }

        [Fact]
        public async Task DeteleteAsyncShouldSucceesIfUserHaveCredentialsAndBookExists()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.DeteleteAsync(UserId, BookId);

            var deletedBook = db.Books.Find(BookId);

            //Assert
            result
                .Should()
                .BeTrue();

            deletedBook
                .Should()
                .BeNull();
        }

        [Fact]
        public async Task DeteleteAsyncShouldFailedIfUserDoesntHaveCredentialsOrBookDoesNotExists()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.DeteleteAsync("john", BookId);

            var deletedBook = db.Books.Find(BookId);

            //Assert
            result
                .Should()
                .BeFalse();

            deletedBook
                .Should()
                .NotBeNull();
        }

        [Fact]
        public async Task TotalAsyncShouldReturnCorrectResults()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.TotalAsync();

            var deletedBook = db.Books.Find(BookId);

            //Assert
            result
                .Should()
                .Be(3);
        }

        [Fact]
        public async Task ExistsAsyncShouldReturnTrueIfBookByGivenParametersExists()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.ExistsAsync(UserId, BookId);

            //Assert
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task ExistsAsyncShouldReturnFalseIfBookByGivenParametersDoesntExists()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.ExistsAsync("saafa", BookId);

            //Assert
            result
                .Should()
                .BeFalse();
        }

        [Fact]
        public async Task GetCoverPictureShouldReturnCoverPictureIfExists()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.GetCoverPicture(BookId);

            //Assert
            result
                .Should()
                .NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetCoverPictureShouldReturnNullIfDoesntExists()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.GetCoverPicture(2);

            //Assert
            result
                .Should()
                .BeNull();
        }

        [Fact]
        public async Task FindTitleShouldReturnTitleIfBookExists()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = bookService.FindTitle(BookId);

            //Assert
            result
                .Should()
                .BeEquivalentTo("Third Book");
        }

        [Fact]
        public async Task FindTitleShouldReturnNukkIfBookDoesntExists()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = bookService.FindTitle(4);

            //Assert
            result
                .Should()
                .BeNull();
        }

        [Fact]
        public async Task FindBookTraderAsyncShouldReturnTraiderIdIfBookExists()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.FindBookTraderAsync(BookId);

            //Assert
            result
                .Should()
                .BeEquivalentTo(UserId);
        }

        [Fact]
        public async Task FindBookTraderAsyncShouldReturnNullIfBookDoesntExists()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var authorService = new AuthorService(db);
            var publisherService = new PublisherService(db);

            var bookService = new BookService(db, authorService, publisherService);

            //Act
            var result = await bookService.FindBookTraderAsync(4);

            //Assert
            result
                .Should()
                .BeNull();
        }

        private async Task<BookStoreDbContext> GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var db = new BookStoreDbContext(dbOptions);

            return db;
        }

        private async Task SeedDb(BookStoreDbContext db)
        {
            var firstBook = new Book { Id = 1, Title = "First Book", Price = 10, TraderId = UserId };
            var secondBook = new Book { Id = 2, Title = "Second Book", Price = 11, TraderId = "penka" };
            var thirdBook = new Book { Id = 3, Title = "Third Book", Price = 12, TraderId = UserId, CoverPicture = new byte[] { 1, 2, 3, 4 } };

            db.AddRange(firstBook, secondBook, thirdBook);

            await db.SaveChangesAsync();
        }
    }
}
