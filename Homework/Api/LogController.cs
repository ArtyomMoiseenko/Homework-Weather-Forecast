using Homework.Database.DAL.UnitOfWork;
using Homework.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Homework.Api
{
    public class LogController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Log
        [HttpGet]
        public async Task<IHttpActionResult> GetLogs()
        {
            var log = new List<LogModel>();
            var forecasts = await _unitOfWork.ForecastRepository.Get();
            var forecastsweather = forecasts.ToList();
            foreach (var item in forecasts)
            {
                var logData = await _unitOfWork.HistoryRepository.FindById(item.HistoryQueryId);
                log.Add(new LogModel()
                {
                    Ip = logData.Ip,
                    City = logData.City,
                    Date = logData.Date,
                    Temperature = item.Temperature,
                    Humidity = item.Humidity,
                    Pressure = item.Pressure,
                    Clouds = item.Clouds,
                    SpeedWind = item.SpeedWind,
                    DescriptionWeather = item.DescriptionWeather
                });
            }
            if (log == null)
            {
                return NotFound();
            }

            return Ok(log);
        }
    }
}
