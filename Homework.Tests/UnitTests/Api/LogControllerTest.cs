using Homework.Api;
using Homework.Database.DAL.UnitOfWork;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Homework.Tests.UnitTests.Api
{
    [TestFixture]
    public class LogControllerTest
    {
        private Mock<IUnitOfWork> mockUnitOfWork;
        private LogController controller;

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
        public void GetLogsResponseIsNotNull()
        {
            // Arrange
            controller = new LogController(mockUnitOfWork.Object);

            // Act
            var result = controller.GetLogs();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
