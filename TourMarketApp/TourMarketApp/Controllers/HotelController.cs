using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Entities;
using TourMarket.Interfaces;

namespace TourMarketApp.Controllers
{
    public class HotelController : BaseController
    {
        private readonly IHotelService hotelService;
        private readonly ICityService cityService;

        public HotelController(IHotelService hotelService, 
            ICityService cityService, IUserService userService)
            :base(userService)
        {
            this.hotelService = hotelService;
            this.cityService = cityService;
        }

        public IActionResult Create()
        {
            ViewBag.User = GetUser();
            ViewBag.Cities = cityService.GetAllCities();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Hotel hotel)
        {
            ViewBag.User = GetUser();
            ViewBag.Cities = cityService.GetAllCities();
            var creationResult = hotelService.CreateHotel(hotel);
            if (creationResult != null)
            {

            }
            return RedirectToAction("hotels", "settings");
        }

        public IActionResult Edit(Guid hotelId)
        {
            ViewBag.User = GetUser();
            var hotel = hotelService.GetHotel(hotelId);

            ViewBag.Cities = cityService.GetAllCities();
            return View(hotel);
        }
        [HttpPost]
        public IActionResult Edit(Hotel hotel)
        {
            ViewBag.User = GetUser();
            ViewBag.Cities = cityService.GetAllCities();
            var creationResult = hotelService.EditHotel(hotel);
            if (creationResult != null)
            {

            }
            return RedirectToAction("hotels", "settings");
        }

        public IActionResult HotelPlaces(Guid hotelId)
        {
            ViewBag.HotelId = hotelId;
            ViewBag.User = GetUser();
            var places = hotelService.GetHotelPlaces(hotelId);
            return View(places);
        }
        public IActionResult CreateHotelPlaces(Guid hotelId)
        {
            ViewBag.User = GetUser();
            ViewBag.HotelId = hotelId;
            return View();
        }

        [HttpPost]
        public IActionResult CreateHotelPlaces(HotelPlace hotelPlace)
        {
            ViewBag.User = GetUser();
            var result = hotelService.CreateHotelPlace(hotelPlace);
            if (result != null)
            {

            }
            return RedirectToAction("HotelPlaces", new { hotelId = hotelPlace.Hotel.Id });
        }

        public IActionResult EditHotelPlaces(Guid hotelPlaceId)
        {
            ViewBag.User = GetUser();
            var place = hotelService.GetHotelPlace(hotelPlaceId);
            return View(place);
        }
        [HttpPost]
        public IActionResult EditHotelPlaces(HotelPlace hotelPlace)
        {
            ViewBag.User = GetUser();
            var result = hotelService.EditHotelPlace(hotelPlace);
            if (result != null)
            {

            }
            return RedirectToAction("HotelPlaces",new { hotelId=hotelPlace.Hotel.Id });
        }

        public List<Hotel> SearchService(Guid cityId, DateTime dateFrom ,DateTime dateTo)
        {
            return hotelService.SearchService(cityId, dateFrom, dateTo);
        }

        public IActionResult Index(Guid hotelId)
        {
            var user = GetUser();
            ViewBag.User = user;
            var model = hotelService.GetHotel(hotelId);
            if (user?.Role?.Description != "Admin")
                return View(model);
            else
                return RedirectToAction("edit", "hotel", new { hotelId = hotelId });
        }

        public bool IsPlacesExist(Guid hotelId, DateTime dateFrom, DateTime dateTo, int countPlaces)
        {
            return hotelService.IsPlacesExist(hotelId, dateFrom, dateTo, countPlaces);
        }
        public double GetSumOfPlaces(Guid hotelId, DateTime dateFrom, DateTime dateTo, int countPlaces)
        {
            return hotelService.GetSumOfPlaces(hotelId, dateFrom, dateTo, countPlaces);
        }
    }
}