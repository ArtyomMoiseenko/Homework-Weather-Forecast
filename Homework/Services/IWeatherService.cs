using Homework.Models;
using System.Threading.Tasks;

namespace Homework.Services
{
    public interface IWeatherService
    {
        Task<string> GetJsonResponse(string city, string countDays);
        Task<WeatherModel> GetWeatherCity(string city, string countDays);
    }
}
