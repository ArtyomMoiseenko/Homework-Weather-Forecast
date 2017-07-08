using Homework.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Homework.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly string _apiKey;

        public WeatherService()
        {
            _apiKey = WebConfigurationManager.AppSettings["ApiKey"];
        }

        public async Task<WeatherModel> GetWeatherCity(string city, string countDays)
        {
            var url = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&units=metric&cnt={countDays}&appid={_apiKey}";
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<WeatherModel>(json);
            var nameIcon = weather.List.FirstOrDefault().Weather.FirstOrDefault().Icon;
            weather.List.FirstOrDefault().Weather.FirstOrDefault().Icon = $"http://openweathermap.org/img/w/{nameIcon}.png";

            return weather;
        }
    }
}