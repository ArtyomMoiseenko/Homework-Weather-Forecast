using System;
using System.Collections.Generic;

namespace Homework.Database.Entities
{
    public class HistoryQuery
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Forecast> Forecasts { get; set; }

        public HistoryQuery()
        {
            Forecasts = new List<Forecast>();
        }
    }
}