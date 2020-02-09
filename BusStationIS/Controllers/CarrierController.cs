using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationIS.Data.Models;
using BusStationIS.Data.ServiceSpecification;
using BusStationIS.Models.Carrier;
using BusStationIS.Models.CarrierViewModel;
using BusStationIS.Models.City;
using Microsoft.AspNetCore.Mvc;

namespace BusStationIS.Controllers
{
    public class CarrierController : Controller
    {
        private readonly ICarrier _carrierService;
        private readonly ICity _cityService;

        public CarrierController(ICarrier carrierService,ICity cityService)
        {
            _carrierService = carrierService;
            _cityService = cityService;
        }


        public IActionResult Create()
        {
            var cities = _cityService.GetAll();

            var allCities = cities.Select(c => new City
            {
                Id = c.Id,
                Name = c.Name
            });

            var model = new CarrierInputModel
            {
               Cities = allCities
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create([Bind]CarrierInputModel carrier)
        {
            if (!ModelState.IsValid)
            {
                TempData["msg"] = "Model is not valid!";
                return Create();
            }
            City newCity = _cityService.GetByName(carrier.CityName);
            Address newAddress = new Address
            {
                StreetName = carrier.StreetName,
                StreetNumber = carrier.StreetNumber,
                City = newCity
            };
            Carrier newCarrier = new Carrier
            {
                Name = carrier.Name,
                Address = newAddress
            };

            if (_carrierService.AddNewCarrier(newCarrier))
            {
                TempData["msg"] = "Carrier is created!";
            }
            else
            {
                TempData["msg"] = "Carrier is not created!";
            }

            return RedirectToPage("/..");
        }

        public IActionResult Index()
        {
            var carriers = _carrierService.GetAll();

            var carriersListing = carriers.Select(c => new CarrierDetailModel
            {
                Id = c.Id,
                Name = c.Name,
                Vehicles = c.Vehicles,
                Address = c.Address,
                Contacts = FrontHelpers.FrontHumanizeHelper.ContactsHumanize(c.Contacts)
            });

            var model = new CarrierIndexModel
            {
                Carriers = carriersListing
            };
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var carrier = _carrierService.GetById(id);

            var model = new CarrierDetailModel
            {
                Id = carrier.Id,
                Name = carrier.Name,
                Address = carrier.Address,
                Contacts = FrontHelpers.FrontHumanizeHelper.ContactsHumanize(carrier.Contacts),
                Vehicles = carrier.Vehicles
            };

            return View(model);
        }
    }
}