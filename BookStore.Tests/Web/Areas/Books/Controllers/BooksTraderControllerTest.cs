using BookStore.Data.Models;
using BookStore.Services.Books;
using BookStore.Services.Books.Models;
using BookStore.Services.Users;
using BookStore.Web;
using BookStore.Web.Areas.Books.Controllers;
using FluentAssertions;
using LearningSystem.Test.Mocks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Tests.Web.Areas.Books.Controllers
{
    public class BooksTraderControllerTest
    {
        [Fact]
        public void BooksTraderControllerShouldBeInBooksArea()
        {
            //Arrange
            var controller = typeof(BooksTraderController);

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
                .Be(WebConstants.BooksArea);
        }

        [Fact]
        public void BooksTraderControllerShouldBeForAuthorizedUsers()
        {
            //Arrange
            var controller = typeof(BooksTraderController);

            //Act
            var areaAttribute = controller.GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            //Assert
            areaAttribute
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void GetCreateShouldReturnView()
        {
            //Arrange
            var bookService = new Mock<IBookService>();

            var controller = new BooksTraderController(bookService.Object, UserManagerMock.New.Object);

            //Act
            var result = controller.Create();

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>();
        }

        [Fact]
        public async Task PostCreateShouldReturnViewWithModelIfModelStateIsInvalid()
        {
            //Arrange
            var bookService = new Mock<IBookService>();

            var controller = new BooksTraderController(bookService.Object, UserManagerMock.New.Object);
            controller.ModelState.AddModelError(string.Empty, "Error");

            //Act
            var result = await controller.Create(new BookCreateServiceModel(), null);

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>();
        }

        [Fact]
        public async Task PostCreateShouldReturnRedirectWithValidModel()
        {
            //Arrange
            const string titleValue = "Title";
            const string authorNamesValue = "AuthorName";
            const Category categoryValue = Category.История;
            const Condition conditionValue = Condition.Добро;
            const Coverage coverageValue = Coverage.Твърди;
            const string descriptionValue = "Description";
            const string languageValue = "Language";
            const decimal priceValue = 20;
            const int publicationYearValue = 1967;
            const string publisherNameValue = "PublisherName";

            string modelTitle = null;
            string modelAuthorNames = null;
            string modelPublisherName = null;
            Category modelCategory = Category.Всички;
            int modelPublicationYear = 0;
            decimal modelPrice = 0;
            Condition modelCondition = Condition.Добро;
            string modelLanguage = null;
            byte[] modelCoverPicture = null;
            Coverage modelCoverage = Coverage.Меки;
            string modelDescription = null;
            string modelTraderId = null;
            string successMessage = null;

            var bookService = new Mock<IBookService>();
            bookService
                .Setup(b => b.CreateAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Category>(),
                    It.IsAny<int>(),
                    It.IsAny<decimal>(),
                    It.IsAny<Condition>(),
                    It.IsAny<string>(),
                    It.IsAny<byte[]>(),
                    It.IsAny<Coverage>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Callback((string title,
                          string authorNames,
                          string publisherName,
                          Category category,
                          int publicationYear,
                          decimal price,
                          Condition condition,
                          string language,
                          byte[] coverPicture,
                          Coverage coverage,
                          string description,
                          string traderId) =>
                          {
                              modelTitle = title;
                              modelAuthorNames = authorNames;
                              modelPublisherName = publisherName;
                              modelCategory = category;
                              modelPublicationYear = publicationYear;
                              modelPrice = price;
                              modelCondition = condition;
                              modelLanguage = language;
                              modelCoverPicture = coverPicture;
                              modelCoverage = coverage;
                              modelDescription = description;
                              modelTraderId = traderId;
                          })
                          .ReturnsAsync(1);

            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => successMessage = message as string);

            var controller = new BooksTraderController(bookService.Object, UserManagerMock.New.Object);
            controller.TempData = tempData.Object;

            //Act
            var result = await controller.Create(new BookCreateServiceModel
            {
                Title = titleValue,
                AuthorNames = authorNamesValue,
                Category = categoryValue,
                Condition = conditionValue,
                Coverage = coverageValue,
                Description = descriptionValue,
                Language = languageValue,
                Price = priceValue,
                PublicationYear = publicationYearValue,
                PublisherName = publisherNameValue
            }, null);

            //Assert
            modelTitle.Should().Be(titleValue);
            modelAuthorNames.Should().Be(authorNamesValue);
            modelCategory.Should().Be(categoryValue);
            modelCondition.Should().Be(conditionValue);
            modelCoverage.Should().Be(coverageValue);
            modelDescription.Should().Be(descriptionValue);
            modelCoverPicture.Should().BeNull();
            modelLanguage.Should().Be(languageValue);
            modelPrice.Should().Be(priceValue);
            modelPublicationYear.Should().Be(publicationYearValue);
            modelPublisherName.Should().Be(publisherNameValue);

            successMessage.Should().Be($"Книгата със заглавие {titleValue} е добавена успешно.");
        }
    }
}
