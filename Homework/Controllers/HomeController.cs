using Homework.Database;
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
        private IWeatherService weatherService;

        public HomeController(IWeatherService service)
        {
            weatherService = service;
        }

        public ActionResult Index()
        {
            using (var db = new ForecastWeatherContext())
            {
                ViewBag.Cities = db.Cities.Select(item => item).ToList();
            }
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
                weather = await weatherService.GetWeatherCity(city, countDays);
            }
            catch(Exception)
            {
                return View("Error");
            }
            return PartialView(weather);
        }

        public ActionResult UserQueriesLog()
        {
            IList<LogModel> log;
            using (var db = new ForecastWeatherContext())
            {
                log = new List<LogModel>();
                var forecasts = db.Forecasts.ToList();
                foreach (var item in forecasts)
                {
                    log.Add(new LogModel()
                    {
                        Ip = db.HistoryQueries.FirstOrDefault(i => i.Id == item.HistoryQueryId).Ip,
                        City = db.HistoryQueries.FirstOrDefault(i => i.Id == item.HistoryQueryId).City,
                        Date = db.HistoryQueries.FirstOrDefault(i => i.Id == item.HistoryQueryId).Date,
                        Temperature = item.Temperature,
                        Humidity = item.Humidity,
                        Pressure = item.Pressure,
                        Clouds = item.Clouds,
                        SpeedWind = item.SpeedWind,
                        DescriptionWeather = item.DescriptionWeather
                    });
                }
                    
            }
            return View(log);
        }
    }
}