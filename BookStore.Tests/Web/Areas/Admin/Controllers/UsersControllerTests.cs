using BookStore.Web;
using BookStore.Web.Areas.Admin.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace BookStore.Tests.Web.Areas.Admin.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public void UsersControllerShouldBeInAdminArea()
        {
            //Arrange
            var controller = typeof(UsersController);

            //Act
            var areaAttribute = controller.GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute))
                as AreaAttribute;

            //Assert
            areaAttribute
                .Should()
                .NotBeNull();

            areaAttribute
                .RouteValue.Should()
                .Be(WebConstants.AdminArea);
        }

        [Fact]
        public void UsersControllerShouldBeOnlyForAdmins()
        {
            //Arrange
            var controller = typeof(UsersController);

            //Act
            var areaAttribute = controller.GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            //Assert
            areaAttribute
                .Should()
                .NotBeNull();

            areaAttribute
                .Roles.Should()
                .Be(WebConstants.AdministratorRole);
        }
    }
}
