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

        public ActionResult Index()
        {
            ViewBag.Cities = _unitOfWork.CityRepository.Get().ToList();
            return View();
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

        public ActionResult UserQueriesLog()
        {
            var log = new List<LogModel>();
            var forecasts = _unitOfWork.ForecastRepository.Get().ToList();
            foreach (var item in forecasts)
            {
                log.Add(new LogModel()
                {
                    Ip = _unitOfWork.HistoryRepository.FindById(item.HistoryQueryId).Ip,
                    City = _unitOfWork.HistoryRepository.FindById(item.HistoryQueryId).City,
                    Date = _unitOfWork.HistoryRepository.FindById(item.HistoryQueryId).Date,
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