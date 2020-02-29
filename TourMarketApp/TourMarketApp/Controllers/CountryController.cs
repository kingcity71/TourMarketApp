using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Entities;
using TourMarket.Interfaces;

namespace TourMarketApp.Controllers
{
    public class CountryController : BaseController
    {
        ICountryService _countryService;
        public CountryController(ICountryService countryService, IUserService userService)
            :base(userService)
        {
            _countryService = countryService;
        }

        public IActionResult Create()
        {
            ViewBag.User = GetUser();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Country country)
        {
            ViewBag.User = GetUser();
            var creationResult = _countryService.CreateCountry(country);
            if (creationResult != null)
            {
                ViewBag.ErrorMsg = creationResult;
                return View("/settings/index");
            }
            return RedirectToAction("countries", "settings");
        }

        public IActionResult Edit(Guid countryId)
        {
            ViewBag.User = GetUser();
            var country = _countryService.GetCountry(countryId);
            return View(country);
        }
        [HttpPost]
        public IActionResult Edit(Country country)
        {
            ViewBag.User = GetUser();
            var creationResult = _countryService.EditCountry(country);
            if (creationResult != null)
            {
                ViewBag.ErrorMsg = creationResult;
                return View();
            }
            return RedirectToAction("countries", "settings");
        }
    }
}