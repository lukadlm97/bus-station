using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationIS.Data.Models;
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


        [HttpPost]
        public IActionResult Create([Bind]DepartureInputModel departureInput)
        {
            if (!ModelState.IsValid)
            {
                TempData["msg"] = "Model is not valid!";
                return Create();
            }

            Departure newDeparture = new Departure
            {
                CityFrom = _cityService.GetByName(departureInput.CityFrom),
                CityTo = _cityService.GetByName(departureInput.CityTo),
                Carrier = _carrierService.GetByName(departureInput.Carrier),
                PaymentCategory = _paymentCategoryService.GetByName(departureInput.PaymentCategory),
                Vehicle = _vehicleService.GetByRegistration(departureInput.VehicleRegistration)
            };

            if (false)
            {
                TempData["msg"] = "Departure is created!";
            }
            else
            {
                TempData["msg"] = "Departure is not created!";
            }

            return RedirectToPage("/Index");
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