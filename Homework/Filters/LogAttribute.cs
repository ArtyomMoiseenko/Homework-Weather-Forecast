using Homework.Database.DAL.UnitOfWork;
using Homework.Database.Entities;
using Homework.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Homework.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        [Inject]
        public IUnitOfWork UnitOfWork { private get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            int count = int.Parse(request.Form.Get("countDays"));
            var model = (WeatherModel)((PartialViewResult)filterContext.Result).Model;
            var forecasts = new List<Forecast>();

            var log = new HistoryQuery()
            {
                Ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                City = model.City.Name,
                Date = DateTime.Now
            };
            UnitOfWork.HistoryRepository.Create(log);

            for (int i = 0; i < count; i++)
            {
                var forecast = new Forecast()
                {
                    Temperature = model.List[i].Temp.Day,
                    Pressure = model.List[i].Pressure,
                    Humidity = model.List[i].Humidity,
                    Clouds = model.List[i].Clouds,
                    SpeedWind = model.List[i].Speed,
                    DescriptionWeather = model.List[i].Weather.FirstOrDefault().Description,
                    HistoryQueryId = log.Id
                };
                UnitOfWork.ForecastRepository.Create(forecast);
            }
            UnitOfWork.Save();

            base.OnActionExecuted(filterContext);
        }
    }
}