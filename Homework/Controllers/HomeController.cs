using Homework.Database.DAL.UnitOfWork;
using Homework.Filters;
using Homework.Models;
using Homework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Homework.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IWeatherService service, IUnitOfWork unitOfWork)
        {
            _weatherService = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Index()
        {
            var cities = new List<CityModel>();
            var citiesDB = await _unitOfWork.CityRepository.Get();
            citiesDB.ToList().ForEach(i => cities.Add(new CityModel { Id = i.Id, Name = i.Name }));
            return View(cities);
        }

        [Log]
        [HttpPost]
        public async Task<ActionResult> Weather(string city, string countDays)
        {
            if (String.IsNullOrEmpty(city) || city == "Search city")
            {
                return JavaScript($"window.location = '{Url.Action("Index", "Home")}'");
            }
            var today = DateTime.Today;
            var date = new List<DateTime>();
            for (int i = 0; i < Int32.Parse(countDays); i++)
            {
                date.Add(today);
                today = today.AddDays(1);
            }
            ViewBag.Date = date;
            WeatherModel weather;
            try
            {
                weather = await _weatherService.GetWeatherCity(city, countDays);
            }
            catch(Exception)
            {
                return View("Error");
            }
            return PartialView(weather);
        }

        public async Task<ActionResult> UserQueriesLog()
        {
            var log = new List<LogModel>();
            var forecasts = await _unitOfWork.ForecastRepository.Get();
            foreach (var item in forecasts)
            {
                var logDb = await _unitOfWork.HistoryRepository.FindById(item.HistoryQueryId);
                log.Add(new LogModel()
                {
                    Ip = logDb.Ip,
                    City = logDb.City,
                    Date = logDb.Date,
                    Temperature = item.Temperature,
                    Humidity = item.Humidity,
                    Pressure = item.Pressure,
                    Clouds = item.Clouds,
                    SpeedWind = item.SpeedWind,
                    DescriptionWeather = item.DescriptionWeather
                });
            }
                    
            return View(log);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}