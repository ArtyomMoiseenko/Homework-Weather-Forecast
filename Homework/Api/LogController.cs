using Homework.Database.DAL.UnitOfWork;
using Homework.Models;
using System.Collections.Generic;
using System.Linq;
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
        public IHttpActionResult GetLogs()
        {
            var log = new List<LogModel>();
            var forecasts = _unitOfWork.ForecastRepository.Get().ToList();
            foreach (var item in forecasts)
            {
                log.Add(new LogModel()
                {
                    Ip = _unitOfWork.HistoryRepository.FindById(item.HistoryQueryId).Ip,
                    City = _unitOfWork.HistoryRepository.FindById(item.HistoryQueryId).City,
                    Date = _unitOfWork.HistoryRepository.FindById(item.HistoryQueryId).Date,
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
