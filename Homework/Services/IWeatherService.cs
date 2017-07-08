using Homework.Models;
using System.Threading.Tasks;

namespace Homework.Services
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetWeatherCity(string city, string countDays);
    }
}
