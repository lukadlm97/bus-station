using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationIS.Data.Models;
using BusStationIS.Data.ServiceSpecification;

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

            var model = new AllCities
            {
                Citys = allCities
            };
            return View(model);
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