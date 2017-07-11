using System;
using System.ComponentModel;

namespace Homework.Models
{
    public class LogModel
    {
        [DisplayName("IP")]
        public string Ip { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        [DisplayName("Speed wind")]
        public double SpeedWind { get; set; }

        [DisplayName("Description weather")]
        public string DescriptionWeather { get; set; }

        public int Humidity { get; set; }

        public int Clouds { get; set; }
    }
}