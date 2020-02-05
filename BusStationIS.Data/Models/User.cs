using System;

namespace BusStationIS.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public virtual Person  Person { get; set; }
        public DateTime MemberSince { get; set; }
        public virtual UserCategory Category { get; set; }
    }
}