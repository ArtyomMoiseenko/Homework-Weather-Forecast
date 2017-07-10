using Homework.Database;
using Homework.Database.Entities;
using Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Homework.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
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
            for (int i = 0; i < count; i++)
            {
                forecasts.Add(new Forecast()
                {
                    Temperature = model.List[i].Temp.Day,
                    Pressure = model.List[i].Pressure,
                    Humidity = model.List[i].Humidity,
                    Clouds = model.List[i].Clouds,
                    SpeedWind = model.List[i].Speed,
                    DescriptionWeather = model.List[i].Weather.FirstOrDefault().Description,
                    HistoryQueryId = log.Id
                });
            }

            using (var db = new ForecastWeatherContext())
            {
                db.HistoryQueries.Add(log);
                db.Forecasts.AddRange(forecasts);
                db.SaveChanges();
            }

            base.OnActionExecuted(filterContext);
        }
    }
}