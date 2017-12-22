using BookStore.Data;
using BookStore.Data.Models;
using BookStore.Services.Implementations;
using BookStore.Services.Orders.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Tests.Services
{
    public class OrderServiceTest
    {
        private const string UserId = "ivan";
        private const int BookId = 1;

        public OrderServiceTest()
        {
            TestStartUp.Initialize();
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectResultsOrderedByIdDescending()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var shoppingCart = new ShoppingCartService(db);
            var orderService = new OrderService(db, shoppingCart);

            //Act
            var result = await orderService.AllAsync();

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
        public async Task MyOrdersAsyncShouldReturnCorrectResults()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var shoppingCart = new ShoppingCartService(db);
            var orderService = new OrderService(db, shoppingCart);

            //Act
            var result = await orderService.MyOrdersAsync(UserId);

            //Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 3
                    && r.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task OrdersFomMeAsyncShouldReturnCorrectResults()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var shoppingCart = new ShoppingCartService(db);
            var orderService = new OrderService(db, shoppingCart);

            //Act
            var result = await orderService.OrdersFomMeAsync(UserId);

            //Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 2)
                .And
                .HaveCount(1);
        }

        [Fact]
        public async Task DetailsAsyncShouldReturnCorrectResults()
        {
            const int OrderId = 2;

            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var shoppingCart = new ShoppingCartService(db);
            var orderService = new OrderService(db, shoppingCart);

            //Act
            var result = await orderService.DetailsAsync(OrderId);

            //Assert
            result
                .Equals(new Order { Id = OrderId, CustomerId = "chocho", TraderId = UserId });
        }

        [Fact]
        public async Task UserInfoAsyncShouldReturnCorrectResults()
        {
            const string Address = "Vitoshka 20";
            const string City = "Plovdiv";
            const string PhoneNumber = "0896";
            const string Email = "some@mail";

            //Arrange
            var db = await this.GetDatabase();
            var user = new User { Id = UserId, Address = Address, City = City, PhoneNumber = PhoneNumber, Email = Email };

            db.Add(user);

            await db.SaveChangesAsync();

            var shoppingCart = new ShoppingCartService(db);
            var orderService = new OrderService(db, shoppingCart);

            //Act
            var result = await orderService.UserInfoAsync(UserId);

            //Assert
            result
                .Equals(new User { Id = UserId, Address = Address, City = City, Email = Email });
        }

        [Fact]
        public async Task OrderBookAsyncShouldReturnTrueIfBookIsOrderable()
        {
            //Arrange
            var db = await this.GetDatabase();

            var firstBook = new Book { Id = 1 };
            var secondBook = new Book { Id = 2 };

            db.AddRange(firstBook, secondBook);
            await db.SaveChangesAsync();

            var bookIds = new List<int>() { 1, 2 };

            var shoppingCart = new ShoppingCartService(db);
            var orderService = new OrderService(db, shoppingCart);

            var dict = new Dictionary<int, int>
            {
                {1, 1 },
                {2, 1 }
            };

            //Act
            var result = await orderService.OrderBookAsync(UserId, bookIds, 20, dict);

            //Assert
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task IsOrderedShouldReturnTrueIfTheBookIsOrdered()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var shoppingCart = new ShoppingCartService(db);
            var orderService = new OrderService(db, shoppingCart);

            //Act
            var result = await orderService.IsOrderedAsync(UserId, BookId);

            //Assert
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task IsOrderedShouldReturnFalseIfTheBookIsNotOrdered()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var shoppingCart = new ShoppingCartService(db);
            var orderService = new OrderService(db, shoppingCart);

            //Act
            var result = await orderService.IsOrderedAsync(UserId, 3);

            //Assert
            result
                .Should()
                .BeFalse();
        }

        [Fact]
        public async Task TotalAsyncShouldReturnCorrectResult()
        {
            //Arrange
            var db = await this.GetDatabase();
            await this.SeedDb(db);

            var shoppingCart = new ShoppingCartService(db);
            var orderService = new OrderService(db, shoppingCart);

            //Act
            var result = await orderService.TotalAsync();

            //Assert
            result
                .Should()
                .Equals(3);
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
            var firstOrder = new Order { Id = 1, CustomerId = UserId, TraderId = "hari" };
            var secondOrder = new Order { Id = 2, CustomerId = "chocho", TraderId = UserId };
            var thirdOrder = new Order { Id = 3, CustomerId = UserId, TraderId = "gari" };

            firstOrder.Books.Add(new OrderBook { BookId = BookId });

            db.AddRange(firstOrder, secondOrder, thirdOrder);

            await db.SaveChangesAsync();
        }
    }
}
