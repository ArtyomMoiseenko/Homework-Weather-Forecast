using Homework.Api;
using Homework.Database.DAL.UnitOfWork;
using Homework.Models;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Homework.Tests.UnitTests.Api
{
    [TestFixture]
    public class CitiesControllerTest
    {
        private Mock<IUnitOfWork> mockUnitOfWork;
        private CitiesController controller;

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
        public void GetCitiesResponseIsNotNull()
        {
            // Arrange
            controller = new CitiesController(mockUnitOfWork.Object);

            // Act
            var result = controller.GetCities();

            // Assert
            Assert.IsNotNull(result);
            mockUnitOfWork.Verify(i => i.CityRepository.Get(), Times.Once());
        }

        [Test]
        public void GetCityResponseIsNotNull()
        {
            // Arrange
            controller = new CitiesController(mockUnitOfWork.Object);
            int id = 1;

            // Act
            var result = controller.GetCity(id);

            // Assert
            Assert.IsNotNull(result);
            mockUnitOfWork.Verify(i => i.CityRepository.FindById(id), Times.Once());
        }

        [Test]
        public void CreateCityResponseIsNotNull()
        {
            // Arrange
            controller = new CitiesController(mockUnitOfWork.Object);
            var city = new CityModel { Id = 10, Name = "New York"};

            // Act
            var result = controller.CreateCity(city);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditCityResponseIsNotNull()
        {
            // Arrange
            controller = new CitiesController(mockUnitOfWork.Object);
            var city = new CityModel { Id = 10, Name = "New York" };

            // Act
            var result = controller.EditCity(city.Id, city);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteCityResponseIsNotNull()
        {
            // Arrange
            controller = new CitiesController(mockUnitOfWork.Object);
            int id = 1;

            // Act
            controller.DeleteCity(id);

            // Assert
            mockUnitOfWork.Verify(i => i.CityRepository
                .Remove(mockUnitOfWork.Object.CityRepository.FindById(id)), Times.Once());
        }
    }
}
