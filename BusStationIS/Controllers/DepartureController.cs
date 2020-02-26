﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusStationIS.Data.Models;
using BusStationIS.Data.ServiceSpecification;
using BusStationIS.Models.Departure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

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
            var departures = _deparatureService.GetAll();

            var departuresListing = departures.Select(d => new DepartureDetailModel
            {
                Id = d.Id,
                Cards = d.Cards,
                NumberOfSeats = d.NumberOfSeats,
                Carrier = d.Carrier,
                CityFrom = d.CityFrom,
                CityTo = d.CityTo,
                Distance = d.Distance,
                PaymentCategory = d.PaymentCategory,
                PriceOfCard = d.PriceOfCard,
                Vehicle = d.Vehicle
            });

            var model = new DepartureIndexModel
            {
                Departures = departuresListing
            };


            return View(model);
        }


        public IActionResult Detail(int id)
        {
            var departure = _deparatureService.GetById(id);

            var model = new DepartureDetailModel
            {
                Id = departure.Id,
                NumberOfSeats = departure.NumberOfSeats,
                CityFrom = departure.CityFrom,
                CityTo = departure.CityTo,
                Carrier = departure.Carrier,
                Distance = departure.Distance,
                PaymentCategory = departure.PaymentCategory,
                PriceOfCard = departure.PriceOfCard,
                Vehicle = departure.Vehicle,
                Cards = departure.Cards
            };

            return View(model);
        }

        public IActionResult MakeSpecDeparture([Bind] SpecificallyDepartureIndex specificallyDeparture)
        {
            return View();
        }

        public IActionResult CreateSpecificallyDeparture()
        {
            var departures = _deparatureService.GetAll();

            var detailsDeparures = departures.Select(x => new SpecificallyDepartureDetail
            {
                Id = x.Id,
                Departure = x
            });

            var model = new SpecificallyDepartureIndex 
            { 
                Departures = _deparatureService.GetAll(),
                SpecificallyDepartureDetails = detailsDeparures
            };

            return View(model);
        }

        public IActionResult GenerateWord(int id)
        {
            Departure departure = _deparatureService.GetById(id);

            using (WordDocument document = new WordDocument())
            {
                document.EnsureMinimal();
                document.LastParagraph.AppendText("Hello Word");
                MemoryStream stream = new MemoryStream();

                document.Save(stream, FormatType.Docx);

                stream.Position = 0;

                return File(stream, "application/msword", "Result.docx");
            }
        }

        public IActionResult GeneratePdf()
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFSharp";

            PdfPage page = document.AddPage();

            XGraphics grf = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            grf.DrawString("Hello World!", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height), XStringFormat.Center);
            MemoryStream stream = new MemoryStream();

     
            document.Save(stream, false);

            stream.Position = 0;

            return File(stream,"application/pdf","HelloPDF.pdf");
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

            if(_cityService.IsSameCity(newDeparture.CityFrom,newDeparture.CityTo))
            {
                TempData["msg"] = "Departure must be between two different cities!";
                return Create();
            }

            newDeparture.Distance = _distanceService.CalculateDistance(newDeparture.CityFrom,newDeparture.CityTo);

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