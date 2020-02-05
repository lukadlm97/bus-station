using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BusStationIS.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext()
        {

        }

        public ApplicationDBContext(DbContextOptions options) : base(options) { }
        

    }
}
