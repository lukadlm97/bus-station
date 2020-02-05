﻿using System.Collections.Generic;

namespace BusStationIS.Data.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public List<Contact>  Contacts { get; set; }
    }
}