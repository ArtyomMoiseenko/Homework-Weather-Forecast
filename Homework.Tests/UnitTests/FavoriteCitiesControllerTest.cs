using Homework.Controllers;
using Homework.Database.DAL.UnitOfWork;
using Homework.Models;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Web.Mvc;

namespace Homework.Tests.UnitTests
{
    [TestFixture]
    public class FavoriteCitiesControllerTest
    {
        private Mock<IUnitOfWork> mockUnitOfWork;
        private FavoriteCitiesController controller;

        [SetUp]
        public void SetUp()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            var fakeData = new FakeUnitOfWork();
            var fakeCityRepository = fakeData.GetStubForCities();
            var fakeForecastsRepository = fakeData.GetStubForForecasts();
            var fakeHistoryQueriesRepository = fakeData.GetStubForHistoryQueries();

            mockUnitOfWork.Setup(i => i.CityRepository.Get()).Returns(fakeCityRepository);
            mockUnitOfWork.Setup(i => i.ForecastRepository.Get()).Returns(fakeForecastsRepository);
            mockUnitOfWork.Setup(i => i.HistoryRepository.Get()).Returns(fakeHistoryQueriesRepository);

            mockUnitOfWork.Setup(i => i.CityRepository.FindById(It.IsAny<int>()))
                .Returns<int>(id => fakeCityRepository.FirstOrDefault(item => item.Id == id));
            mockUnitOfWork.Setup(i => i.ForecastRepository.FindById(It.IsAny<int>()))
                .Returns<int>(id => fakeForecastsRepository.FirstOrDefault(item => item.Id == id));
            mockUnitOfWork.Setup(i => i.HistoryRepository.FindById(It.IsAny<int>()))
                .Returns<int>(id => fakeHistoryQueriesRepository.FirstOrDefault(item => item.Id == id));
        }

        [Test]
        public void IndexCityViewModelIsNotNull()
        {
            // Arrange
            controller = new FavoriteCitiesController(mockUnitOfWork.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public void CreateCitySuccess()
        {
            // Arrange
            controller = new FavoriteCitiesController(mockUnitOfWork.Object);
            var city = new CityModel { Id = 5, Name = "Las Vegas" };

            // Act
            var result = controller.Create(city) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            mockUnitOfWork.Verify(i => i.CityRepository.Create(It.IsAny<Database.Entities.City>()), Times.AtLeastOnce);
        }

        [Test]
        public void EditCitySuccess_When_input_data_is_correct()
        {
            // Arrange
            controller = new FavoriteCitiesController(mockUnitOfWork.Object);
            var city = new CityModel { Id = 1, Name = "Las Vegas" };

            // Act
            var result = controller.Edit(city) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            mockUnitOfWork.Verify(i => i.CityRepository.Update(It.IsAny<Database.Entities.City>()), Times.AtLeastOnce);
        }

        [Test]
        public void DeleteCitySuccess_When_input_data_is_correct()
        {
            // Arrange
            controller = new FavoriteCitiesController(mockUnitOfWork.Object);
            var cityId = 1;

            // Act
            var result = controller.DeletePost(cityId) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            mockUnitOfWork.Verify(i => i.CityRepository.Remove(It.IsAny<Database.Entities.City>()), Times.AtLeastOnce);
        }
    }
}
