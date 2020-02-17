using BusStationIS.Data;
using BusStationIS.Data.Models;
using BusStationIS.Data.ServiceSpecification;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusStationIS.Service
{
    public class DepartureService : IDeparture
    {
        private readonly ApplicationDBContext _context;

        public DepartureService(ApplicationDBContext context)
        {
            _context = context;
        }

        public bool Add(Departure newDeparture)
        {
            newDeparture.Cards = new List<Card>();
            try
            {
                _context.Add(newDeparture);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Departure> GetAll()
        {
            throw new NotImplementedException();
        }

        public Departure GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
