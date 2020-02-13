using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStationIS.Models.City
{
    public class OpenWeatherResponseOutput
    {
        public string Temp { get; set; }
        public string Summary { get; set; }
        public string City { get; set; }
    }
}
