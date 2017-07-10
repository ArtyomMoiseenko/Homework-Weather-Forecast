using Homework.Database.Entities;
using System.Data.Entity;

namespace Homework.Database
{
    public class ForecastWeatherContext : DbContext
    {
        public ForecastWeatherContext() : base("ForecastWeatherContext")
        {
            System.Data.Entity.Database.SetInitializer<ForecastWeatherContext>(new ForecastWeatherInitializer());
            Database.Initialize(true);
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<HistoryQuery> HistoryQueries { get; set; }
        public DbSet<Forecast> Forecasts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            System.Data.Entity.Database.SetInitializer<ForecastWeatherContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}