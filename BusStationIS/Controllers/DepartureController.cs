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
        private readonly IDistance _distanceService;
        private readonly IPriceManager _priceService;

        public DepartureController(IDeparture departureService,ICity cityService,ICarrier carrierService,IPaymentCategory paymentCategoryService,IVehicle vehicleService,IDistance distanceService,IPriceManager priceManager)
        {
            _deparatureService = departureService;
            _cityService = cityService;
            _carrierService = carrierService;
            _paymentCategoryService = paymentCategoryService;
            _vehicleService = vehicleService;
            _distanceService = distanceService;
            _priceService = priceManager;
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

            newDeparture.Distance = _distanceService.CalculateDistance(newDeparture.CityFrom,newDeparture.CityTo);


            if(_cityService.IsSameCity(newDeparture.CityFrom,newDeparture.CityTo))
            {
                TempData["msg"] = "Departure must be between two different cities!";
                return Create();
            }
            //TODO: create service for calculate price of card
            newDeparture.PriceOfCard = _priceService.CalculatePrice(newDeparture.Distance.DistanceBetween, newDeparture.PaymentCategory.Price);

            //TODO: get number of seats by vehicle
            newDeparture.NumberOfSeats = newDeparture.Vehicle.Capacity;

            if (_deparatureService.Add(newDeparture))
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