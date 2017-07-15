using Homework.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Tests.UnitTests
{
    internal class FakeUnitOfWork
    {
        public IEnumerable<City> GetStubForCities()
        {
            var cities = new List<City>
            {
                new City
                {
                    Id = 1, Name = "Kiev"
                },
                new City
                {
                    Id = 2, Name = "Lviv"
                },
                new City
                {
                    Id = 3, Name = "Kharkiv"
                }
            };

            return cities;
        }

        public IEnumerable<HistoryQuery> GetStubForHistoryQueries()
        {
            var historyQueries = new List<HistoryQuery>
            {
                new HistoryQuery
                {
                    Id = 1, City = "Kiev", Ip = "172.25.44.39", Date = DateTime.Now
                },
                new HistoryQuery
                {
                    Id = 2, City = "Lviv", Ip = "172.25.44.30", Date = DateTime.Now
                },
                new HistoryQuery
                {
                    Id = 3, City = "Kharkiv", Ip = "172.25.44.32", Date = DateTime.Now
                }
            };

            return historyQueries;
        }

        public IEnumerable<Forecast> GetStubForForecasts()
        {
            var forecasts = new List<Forecast>
            {
                new Forecast
                {
                    Id = 1,
                    Temperature = 10,
                    Clouds = 10,
                    Humidity = 10,
                    Pressure = 10,
                    SpeedWind = 10,
                    DescriptionWeather = "some weather",
                    HistoryQueryId = GetStubForHistoryQueries().ToList().FirstOrDefault().Id
                },
                new Forecast
                {
                    Id = 2,
                    Temperature = 15,
                    Clouds = 15,
                    Humidity = 15,
                    Pressure = 15,
                    SpeedWind = 15,
                    DescriptionWeather = "some weather",
                    HistoryQueryId = GetStubForHistoryQueries().ToList().FirstOrDefault().Id
                },
                new Forecast
                {
                    Id = 3,
                    Temperature = 5,
                    Clouds = 5,
                    Humidity = 5,
                    Pressure = 5,
                    SpeedWind = 5,
                    DescriptionWeather = "some weather",
                    HistoryQueryId = GetStubForHistoryQueries().ToList().FirstOrDefault().Id
                }
            };

            return forecasts;
        }
    }
}
