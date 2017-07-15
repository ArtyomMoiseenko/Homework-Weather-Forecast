using Homework.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Homework.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration _config;

        public WeatherService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> GetJsonResponse(string city, string countDays)
        {
            var url = $"{_config.BaseUrl}/data/2.5/forecast/daily?q={city}&units=metric&cnt={countDays}&appid={_config.ApiKey}";
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<WeatherModel> GetWeatherCity(string city, string countDays)
        {
            var json = await GetJsonResponse(city, countDays);
            var weather = JsonConvert.DeserializeObject<WeatherModel>(json);
            var nameIcon = weather.List.FirstOrDefault().Weather.FirstOrDefault().Icon;
            weather.List.FirstOrDefault().Weather.FirstOrDefault().Icon = $"{_config.BaseUrl}/img/w/{nameIcon}.png";

            return weather;
        }
    }
}