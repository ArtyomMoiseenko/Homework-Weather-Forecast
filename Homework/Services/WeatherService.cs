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
        private readonly string _baseUrl;

        public WeatherService()
        {
            _apiKey = WebConfigurationManager.AppSettings["ApiKey"];
            _baseUrl = WebConfigurationManager.AppSettings["BaseUrl"];
        }

        public async Task<WeatherModel> GetWeatherCity(string city, string countDays)
        {
            var url = $"{_baseUrl}/data/2.5/forecast/daily?q={city}&units=metric&cnt={countDays}&appid={_apiKey}";
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<WeatherModel>(json);
            var nameIcon = weather.List.FirstOrDefault().Weather.FirstOrDefault().Icon;
            weather.List.FirstOrDefault().Weather.FirstOrDefault().Icon = $"{_baseUrl}/img/w/{nameIcon}.png";

            return weather;
        }
    }
}