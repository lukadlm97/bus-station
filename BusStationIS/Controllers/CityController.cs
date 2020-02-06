using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationIS.Data.ServiceSpecification;
using BusStationIS.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusStationIS.Controllers
{
    public class CityController : Controller
    {
        private readonly ICity _cityService;
        private readonly IBusStation _busStationService;
        private readonly ICarrier _carrierService;

        public CityController(ICity cityService,IBusStation busStationService,ICarrier carrierService)
        {
            _cityService = cityService;
            _busStationService = busStationService;
            _carrierService = carrierService;
        }
        public IActionResult Index()
        {
            var citys = _cityService.GetAll();

            var cityListing = citys.Select(c => new CityDetailModel
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                ZipCode = c.ZipCode,
                Carriers = _carrierService.GetByCity(c),
                BusStations = _busStationService.GetByCity(c)
            });

            var model = new CityIndexModel
            {
                Cities = cityListing
            };

            return View(model);
        }
    }
}