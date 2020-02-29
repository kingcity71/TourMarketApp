using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Entities;
using TourMarket.Interfaces;

namespace TourMarketApp.Controllers
{
    public class CityController : BaseController
    {
        ICityService _cityService;
        ICountryService _countryService;
        public CityController(ICityService cityService, ICountryService countryService, IUserService userService):base(userService)
        {
            _cityService = cityService;
            _countryService = countryService;
        }

        public IActionResult Create()
        {
            ViewBag.User = GetUser();
            ViewBag.Countries = _countryService.GetAllCountries();
            return View();
        }

        [HttpPost]
        public IActionResult Create(City city)
        {
            ViewBag.User = GetUser();
            ViewBag.Countries = _countryService.GetAllCountries();
            var creationResult = _cityService.CreateCity(city);
            if (creationResult != null)
            {

            }
            return RedirectToAction("cities", "settings");
        }

        public IActionResult Edit(Guid cityId)
        {
            ViewBag.User = GetUser();
            ViewBag.Countries = _countryService.GetAllCountries();
            var city = _cityService.GetCity(cityId);
            return View(city);
        }
        [HttpPost]
        public IActionResult Edit(City city)
        {
            ViewBag.User = GetUser();
            ViewBag.Countries = _countryService.GetAllCountries();
            var creationResult = _cityService.EditCity(city);
            if (creationResult != null)
            {

            }
            return RedirectToAction("cities", "settings");
        }
    }
}