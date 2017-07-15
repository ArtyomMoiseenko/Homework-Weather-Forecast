using NUnit.Framework;
using Homework.Services;
using Moq;
using System.Threading.Tasks;

namespace Homework.Tests.UnitTests
{
    [TestFixture]
    public class WeatherServiceTest
    {
        private Mock<IConfiguration> mockConfig;
        private Mock<IWeatherService> mockService;

        [SetUp]
        public void SetUp()
        {
            mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(i => i.BaseUrl).Returns("http://api.openweathermap.org");
            mockConfig.Setup(i => i.ApiKey).Returns("ca3f6cacb68fec92e7e0d9a1f67d11dc");

            //mockService = new Mock<IWeatherService>(mockConfig.Object);
            //mockService.Setup(i => i.GetJsonResponse(It.IsAny<string>(), It.IsAny<string>()))
            //    .Returns(It.IsAny<Task<string>>());
        }

        [Test]
        public void JsonResultResponseIsNotNull()
        {
            // Arrange
            var service = new WeatherService(mockConfig.Object);
            string cityName = "Kiev";
            string countDays = "1";

            // Act
            var result = service.GetJsonResponse(cityName, countDays);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Exception);
        }

        [Test]
        public void WeatherResultResponseIsNotNull()
        {
            // Arrange
            var service = new WeatherService(mockConfig.Object);
            string cityName = "Kiev";
            string countDays = "1";

            // Act
            var result = service.GetWeatherCity(cityName, countDays);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Exception);
        }
    }
}
