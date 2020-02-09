using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationIS.Data.ServiceSpecification;

using BusStationIS.Models.CarrierViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BusStationIS.Controllers
{
    public class CarrierController : Controller
    {
        private readonly ICarrier _carrierService;


        public CarrierController(ICarrier carrierService)
        {
            _carrierService = carrierService;
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