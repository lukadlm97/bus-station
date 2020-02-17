using BusStationIS.Data;
using BusStationIS.Data.Models;
using BusStationIS.Data.Models.Weather;
using BusStationIS.Data.ServiceSpecification;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BusStationIS.Service
{
    public class DistanceService:IDistance
    {
        private readonly ApplicationDBContext _context;

        public DistanceService(ApplicationDBContext context)
        {
            _context = context;
        }

        public Distance CalculateDistance(City cityForm, City cityTo)
        {
            Distance distance = new Distance
            {
                CityFrom = cityForm,
                CityTo = cityTo
            };

            


            return distance;
        }

       
        
    }
}
