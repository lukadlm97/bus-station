using BusStationIS.Data;
using BusStationIS.Data.Models;
using BusStationIS.Data.ServiceSpecification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusStationIS.Service
{
    public class CityService : ICity
    {
        private readonly ApplicationDBContext _context;

        public CityService(ApplicationDBContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<City> GetAll()
        {
            return _context.City;
        }

        public City GetById(int id)
        {
            return GetAll()
                .FirstOrDefault(c => c.Id == id);
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
