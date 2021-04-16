using System;
using System.Collections.Generic;
using System.Text;

namespace AccuWeatherConsole.Models
{
    public class Metric
    {
        public string Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }
}
