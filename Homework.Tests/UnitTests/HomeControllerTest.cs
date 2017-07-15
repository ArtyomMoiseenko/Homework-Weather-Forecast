using Homework.Controllers;
using Homework.Database.DAL.UnitOfWork;
using Homework.Models;
using Homework.Services;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Homework.Tests.UnitTests
{
    [TestFixture]
    public class HomeControllerTest
    {
        private Mock<IWeatherService> mockService;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private HomeController controller;

        [SetUp]
        public void SetUp()
        {
            mockService = new Mock<IWeatherService>();
            mockService.Setup(i => i.GetWeatherCity(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new WeatherModel());

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
        public void IndexHomeViewModelIsNotNull()
        {
            // Arrange
            controller = new HomeController(mockService.Object, mockUnitOfWork.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public async Task WeatherViewModelIsNotNull()
        {
            // Arrange
            controller = new HomeController(mockService.Object, mockUnitOfWork.Object);
            string nameCity = "Kiev";
            string countDays = "1";

            // Act
            var result = await controller.Weather(nameCity, countDays) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public void UserQueriesLogViewModelIsNotNull()
        {
            // Arrange
            controller = new HomeController(mockService.Object, mockUnitOfWork.Object);

            // Act
            ViewResult result = controller.UserQueriesLog() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }


    }
}
