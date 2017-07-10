namespace Homework.Database.Entities
{
    public class Forecast
    {
        public int Id { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double SpeedWind { get; set; }
        public string DescriptionWeather { get; set; }
        public int Humidity { get; set; }
        public int Clouds { get; set; }

        public int? HistoryQueryId { get; set; }
        public virtual HistoryQuery HistoryQuery { get; set; }
    }
}