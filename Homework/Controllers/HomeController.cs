using Homework.Models;
using Homework.Services;
using System;
using System.Collections.Generic;
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
            return View();
        }

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
    }
}