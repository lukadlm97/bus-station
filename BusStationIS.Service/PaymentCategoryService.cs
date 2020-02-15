using BusStationIS.Data;
using BusStationIS.Data.Models;
using BusStationIS.Data.ServiceSpecification;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusStationIS.Service
{
    public class PaymentCategoryService : IPaymentCategory
    {
        private readonly ApplicationDBContext _context;

        public PaymentCategoryService(ApplicationDBContext context)
        {
            _context = context;
        }
        public IEnumerable<PaymentCategory> GetAll()
        {
            return _context.PaymentCategories;
        }
    }
}
