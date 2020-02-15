using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationIS.Data.ServiceSpecification;
using BusStationIS.Models.Departure;
using Microsoft.AspNetCore.Mvc;

namespace BusStationIS.Controllers
{
    public class DepartureController : Controller
    {
        private readonly IDeparture _deparatureService;
        private readonly ICity _cityService;
        private readonly ICarrier _carrierService;
        private readonly IPaymentCategory _paymentCategoryService;
        private readonly IVehicle _vehicleService;

        public DepartureController(IDeparture departureService,ICity cityService,ICarrier carrierService,IPaymentCategory paymentCategoryService,IVehicle vehicleService)
        {
            _deparatureService = departureService;
            _cityService = cityService;
            _carrierService = carrierService;
            _paymentCategoryService = paymentCategoryService;
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var cities = _cityService.GetAll();
            var carriers = _carrierService.GetAll();
            var paymentCategory = _paymentCategoryService.GetAll();
            var vehicles = _vehicleService.GetAll();

            var model = new DepartureInputModel
            {
                Cities = cities,
                Carriers = carriers,
                PaymentCategories = paymentCategory,
                Vehicles = vehicles
            };
            return View(model);
        }
    }
}