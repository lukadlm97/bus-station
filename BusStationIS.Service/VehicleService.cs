using BusStationIS.Data;
using BusStationIS.Data.Models;
using BusStationIS.Data.ServiceSpecification;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusStationIS.Service
{
    public class VehicleService : IVehicle
    {
        private readonly ApplicationDBContext _context;

        public VehicleService(ApplicationDBContext context)
        {
            _context = context;
        }
        public IEnumerable<VehicleType> GetVehicleTypes()
        {
            return (IEnumerable<VehicleType>)Enum.GetValues(typeof(VehicleType));
        }
    }
}
