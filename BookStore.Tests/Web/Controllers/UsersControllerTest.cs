using BookStore.Services.Models;
using BookStore.Services.Users;
using BookStore.Services.Users.Implementations;
using BookStore.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Tests.Web.Controllers
{
    public class UsersControllerTest
    {
        [Fact]
        public async Task ProfileShouldReturnNotFoundWithInvalidUsername()
        {
            //Arrange
            var users = Mock.Of<IUserService>();
            var controller = new UsersController(users);

            //Act
            var result = await controller.Profile("somename");

            //Assert
            result
                .Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task ProfileShouldReturnViewWithModelWithCorrectUserNameGiven()
        {
            //Arrange
            const string userName = "somename";

            var users = new Mock<IUserService>();
            users
                .Setup(u => u.ProfileAsync(It.IsAny<string>()))
                .ReturnsAsync(new UserProfileServiceModel { UserName = userName });

            var controller = new UsersController(users.Object);

            //Act
            var result = await controller.Profile(userName);

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<UserProfileServiceModel>().UserName == userName);
        }
    }
}
