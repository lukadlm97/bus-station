using BusStationIS.Data;
using BusStationIS.Data.Models;
using BusStationIS.Data.ServiceSpecification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusStationIS.Service
{
    public class BusStationService:IBusStation
    {
        private readonly ApplicationDBContext _context;

        public BusStationService(ApplicationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<BusStation> GetAll()
        {
            return _context.BusStations
                .Include(b => b.Address)
                    .ThenInclude(a => a.City);
        }

        public IEnumerable<BusStation> GetByCity(City city)
        {
            return GetAll()
                .Where(b => b.Address.City == city)
                .DefaultIfEmpty();
        }

        public BusStation GetById(int id)
        {
            return GetAll()
                .FirstOrDefault(b => b.Id == id);
        }
    }
}
