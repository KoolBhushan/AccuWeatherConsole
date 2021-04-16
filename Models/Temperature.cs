using System;
using System.Collections.Generic;
using System.Text;

namespace AccuWeatherConsole.Models
{
    public class Temperature
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }
}
