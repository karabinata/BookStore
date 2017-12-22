using BookStore.Data;
using BookStore.Data.Models;
using BookStore.Services.Authors.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Tests.Services
{
    public class AuthorServiceTest
    {
        public AuthorServiceTest()
        {
            TestStartUp.Initialize();
        }

        [Fact]
        public async Task CreateAsyncShouldAddToDbAuthorsIfTheyDontExist()
        {
            //Arrange
            var db = await this.GetDatabase();

            var authorService = new AuthorService(db);

            var authors = new List<string>() { "Ivan Vazov", "Stivun King", "Pitur Strob" };
            //Act

            var result = await authorService.CreateAsync(authors);

            //Arrange
            result
                .Should()
                .HaveCount(3);
        }

        [Fact]
        public async Task CreateAsyncShouldNotAddToDbAuthorsIfTheyExist()
        {
            //Arrange
            var db = await this.GetDatabase();

            var authorService = new AuthorService(db);

            var firstAuthor = new Author { Name = "Ivan Vazov" };
            var secondAuthor = new Author { Name = "Stivun King" };
            var thirdAuthor = new Author { Name = "Pitur Strob" };

            db.AddRange(firstAuthor, secondAuthor, thirdAuthor);

            await db.SaveChangesAsync();

            var authors = new List<string>() { "Ivan Vazov", "Stivun King", "Pitur Strob" };
            //Act

            var result = await authorService.CreateAsync(authors);

            //Arrange
            result
                .Should()
                .HaveCount(3);
        }

        private async Task<BookStoreDbContext> GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var db = new BookStoreDbContext(dbOptions);

            return db;
        }
    }
}
