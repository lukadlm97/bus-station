using BusStationIS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStationIS.Models
{
    public class CityDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<Carrier> Carriers { get; set; }
        public IEnumerable<BusStation> BusStations { get; set; }
    }
}
