using Homework.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace Homework.Api
{
    public class ForecastWeatherController : ApiController
    {
        private readonly IWeatherService _service;

        public ForecastWeatherController(IWeatherService service)
        {
            _service = service;
        }
        
        // GET: api/ForecastWeather
        [HttpGet]
        public async Task<IHttpActionResult> GetWeather(string nameCity, int countDays)
        {
            var city = await _service.GetWeatherCity(nameCity, countDays.ToString());
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }
    }
}
