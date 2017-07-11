using System;

namespace Homework.Models
{
    public class LogModel
    {
        public string Ip { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double SpeedWind { get; set; }
        public string DescriptionWeather { get; set; }
        public int Humidity { get; set; }
        public int Clouds { get; set; }
    }
}