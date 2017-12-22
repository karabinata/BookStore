using BookStore.Data;
using BookStore.Data.Models;
using BookStore.Services.Publishers.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Tests.Services
{
    public class PublisherServiceTest
    {
        public PublisherServiceTest()
        {
            TestStartUp.Initialize();
        }

        [Fact]
        public async Task CreateAsyncShouldAddToDbPublisherIfHeDontExist()
        {
            //Arrange
            var db = await this.GetDatabase();

            var publisherService = new PublisherService(db);

            var publisher = "Enigma";

            //Act
            var result = await publisherService.CreateAsync(publisher);

            //Arrange
            result
                .Should()
                .Equals(1);
        }

        [Fact]
        public async Task CreateAsyncShouldNotAddToDbPublisherIfHeExist()
        {
            //Arrange
            var db = await this.GetDatabase();

            var publisherService = new PublisherService(db);

            var publisherName = new Publisher { Name = "Enigma" };

            db.Add(publisherName);

            await db.SaveChangesAsync();

            var publisher = "Enigma";
            //Act
            var result = await publisherService.CreateAsync(publisher);

            //Arrange
            result
                .Should()
                .Equals(1);
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
