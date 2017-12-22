using BookStore.Data;
using BookStore.Data.Models;
using BookStore.Services.Users.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Tests.Services
{
    public class UserServiceTest
    {
        private const string UserId = "ivan";
        private const string Address = "Pencho Slaveikov 20";

        [Fact]
        public async Task DetailsAsyncShouldReturnCorrectResult()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var userService = new UserService(db);

            //Act
            var result = userService.DetailsAsync(UserId);

            //Assert
            result
                .Id
                .Should()
                .Equals(UserId);
        }

        [Fact]
        public async Task ProfileAsyncShouldReturnCorrectResult()
        {
            const string UserName = "ivanushka";

            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var userService = new UserService(db);

            //Act
            var result = userService.ProfileAsync(UserName);

            //Assert
            result
                .Id
                .Should()
                .Equals(UserId);
        }

        [Fact]
        public async Task UpdateOrderInfoAsyncShouldUpdateIfNecessary()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var userService = new UserService(db);

            //Act
            var result = userService.UpdateOrderInfoAsync(UserId, Address, "Plovdiv", "5544");

            //Assert
            result
                .Equals(new User { Id = UserId, Address = Address, City = "Plovdiv", PhoneNumber = "5544" });
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
            var firstUser = new User { Id = "ivan", UserName = "ivanushka", Address = Address };
            var secondUser = new User { Id = "petur", UserName = "petriov" };
            var thirdUser = new User { Id = "canko", UserName = "cankovich" };

            db.AddRange(firstUser, secondUser, thirdUser);

            await db.SaveChangesAsync();
        }
    }
}
