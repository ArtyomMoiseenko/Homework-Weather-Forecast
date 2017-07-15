using Homework.Controllers;
using Homework.Database.DAL.UnitOfWork;
using Homework.Services;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Data.Entity;
using Homework.Database;
using System.Web.Mvc;

namespace Homework.Tests.IntegrationTests
{
    [TestFixture]
    public class HomeControllerIntegrationTest
    {
        private IUnitOfWork unitOfWork;
        private HomeController controller;
        private IWeatherService service;
        private DbContext context;

        [SetUp]
        public void SetUp()
        {
            service = new WeatherService(new Configuration
            {
                BaseUrl = "http://api.openweathermap.org",
                ApiKey = "ca3f6cacb68fec92e7e0d9a1f67d11dc"
            });
            context = new ForecastWeatherContext();
            unitOfWork = new UnitOfWork(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
            unitOfWork.Dispose();
        }

        [Test]
        public void IndexWeatherViewModelIsNotNull_When_data_is_real()
        {
            // Arrange
            controller = new HomeController(service, unitOfWork);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public async Task WeatherViewModelIsNotNull_When_data_is_real()
        {
            // Arrange
            IWeatherService service = new WeatherService(new Configuration
            {
                BaseUrl = "http://api.openweathermap.org",
                ApiKey = "ca3f6cacb68fec92e7e0d9a1f67d11dc"
            });
            string cityName = "Kiev";
            string countDays = "1";
            controller = new HomeController(service, unitOfWork);

            // Act
            var result = await controller.Weather(cityName, countDays) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public void UserQueriesLogViewModelIsNotNull_When_data_is_real()
        {
            // Arrange
            controller = new HomeController(service, unitOfWork);

            // Act
            var result = controller.UserQueriesLog() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }
    }
}
