using Homework.Controllers;
using Homework.Database;
using Homework.Database.DAL.UnitOfWork;
using Homework.Models;
using Homework.Services;
using NUnit.Framework;
using System.Data.Entity;
using System.Web.Mvc;

namespace Homework.Tests.IntegrationTests
{
    [TestFixture]
    public class FavoriteCitiesControllerIntegrationTest
    {
        private IUnitOfWork unitOfWork;
        private FavoriteCitiesController controller;
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
        public void IndexCityViewModelIsNotNull_When_data_is_real()
        {
            // Arrange
            controller = new FavoriteCitiesController(unitOfWork);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public void CreateCitySuccess_When_data_is_real()
        {
            // Arrange
            controller = new FavoriteCitiesController(unitOfWork);
            var city = new CityModel { Id = 5, Name = "Las Vegas" };

            // Act
            var result = controller.Create(city) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void EditCitySuccess_When_data_is_real()
        {
            // Arrange
            controller = new FavoriteCitiesController(unitOfWork);
            var city = new CityModel { Id = 2, Name = "Lviv" };

            // Act
            var result = controller.Edit(city) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void DeleteCitySuccess_When_data_is_real()
        {
            // Arrange
            controller = new FavoriteCitiesController(unitOfWork);
            var cityId = 6;

            // Act
            var result = controller.DeletePost(cityId) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
