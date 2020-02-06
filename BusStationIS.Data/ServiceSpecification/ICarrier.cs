using BusStationIS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusStationIS.Data.ServiceSpecification
{
    public interface ICarrier
    {
        Carrier GetById(int id);
        IEnumerable<Carrier> GetAll();
        IEnumerable<Carrier> GetByCity(City city);
    }
}
