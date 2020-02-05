﻿using BusStationIS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusStationIS.Data.ServiceSpecification
{
    public interface ICity
    {
        IEnumerable<City> GetAll();
        City GetById(int id);
        void Update(int id);
        void Delete(int id);
    }
}
