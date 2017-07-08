using Homework.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Homework.Services
{
    public class WeatherService
    {
        private static IDictionary<string, string> citiesId;
        private const string APPID = "ca3f6cacb68fec92e7e0d9a1f67d11dc";

        public static IDictionary<string, string> CitiesId { get { return citiesId; } }

        public WeatherService()
        {
            if(citiesId == null)
            {
                citiesId = new Dictionary<string, string>();
                citiesId.Add("Киев", "703448");
                citiesId.Add("Львов", "702550");
                citiesId.Add("Харьков", "706483");
                citiesId.Add("Днепропетровск", "703448");
                citiesId.Add("Одесса", "698740");
            }
        }

        public WeatherModel GetWeatherCity(string city, string countDays)
        {
            var url = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&units=metric&cnt={countDays}&appid={APPID}";
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var responseText = reader.ReadToEnd();
            var weather = JsonConvert.DeserializeObject<WeatherModel>(responseText);
            var icon = weather.List.FirstOrDefault().Weather.FirstOrDefault().Icon;
            weather.List.FirstOrDefault().Weather.FirstOrDefault().Icon = $"http://openweathermap.org/img/w/{icon}.png";

            return weather;
        }
    }
}