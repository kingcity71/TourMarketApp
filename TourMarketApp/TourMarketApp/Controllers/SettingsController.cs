using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Interfaces;

namespace TourMarketApp.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly ICityService cityService;
        private readonly IHotelService hotelService;
        private readonly ICountryService countryService;

        public SettingsController(ICityService cityService, IHotelService hotelService, ICountryService countryService, IUserService userService)
            :base(userService)
        {
            this.cityService = cityService;
            this.hotelService = hotelService;
            this.countryService = countryService;
        }

        public IActionResult Index()
        {
            ViewBag.User = GetUser();
            return View();
        }
        public IActionResult Cities()
        {
            ViewBag.User = GetUser();
            var cities = cityService.GetAllCities();
            return View(cities);
        }
        public IActionResult Countries()
        {
            ViewBag.User = GetUser();
            var countries = countryService.GetAllCountries();
            return View(countries);
        }
        public IActionResult Hotels()
        {
            ViewBag.User = GetUser();
            var hotels = hotelService.GetAllHotels();
            return View(hotels);
        }
    }
}