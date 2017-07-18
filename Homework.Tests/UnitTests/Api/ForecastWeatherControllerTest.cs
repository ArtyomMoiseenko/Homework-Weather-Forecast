using Homework.Api;
using Homework.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Homework.Tests.UnitTests.Api
{
    [TestFixture]
    public class ForecastWeatherControllerTest
    {
        private Mock<IWeatherService> mockService;
        private ForecastWeatherController controller;

        [SetUp]
        public void SetUp()
        {
            mockService = new Mock<IWeatherService>();
            mockService.Setup(i => i.GetJsonResponse(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(It.IsAny<Task<string>>());
        }


        [Test]
        public void GetWeatherResponseIsNotNull()
        {
            // Arrange
            controller = new ForecastWeatherController(mockService.Object);
            string nameCity = "Kiev";
            int countDays = 3;

            // Act
            var result = controller.GetWeather(nameCity, countDays);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
