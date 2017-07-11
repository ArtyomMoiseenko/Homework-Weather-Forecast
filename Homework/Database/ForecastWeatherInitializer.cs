using Homework.Database.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace Homework.Database
{
    public class ForecastWeatherInitializer : CreateDatabaseIfNotExists<ForecastWeatherContext>
    {
        protected override void Seed(ForecastWeatherContext context)
        {
            var cities = new List<City>
            {
                new City() { Name = "Kiev" },
                new City() { Name = "Lviv" },
                new City() { Name = "Kharkiv" },
                new City() { Name = "Odessa" },
                new City() { Name = "Dnepr" }
            };

            cities.ForEach(city => context.Cities.Add(city));
            context.SaveChanges();
        }
    }
}