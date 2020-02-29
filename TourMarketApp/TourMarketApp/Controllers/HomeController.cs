using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourMarket.DAL;
using TourMarket.Entities;
using TourMarket.Interfaces;
using TourMarketApp.Models;

namespace TourMarketApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly Context con;
        private readonly ICommonService commonService;
        private readonly IHotelService hotelService;

        public HomeController(Context con, ICommonService commonService, IHotelService hotelService, IUserService userService)
            :base(userService)
        {
            this.con = con;
            this.commonService = commonService;
            this.hotelService = hotelService;
        }
        
        

        public IActionResult Index()
        {
            IntitialCreate();
            ViewBag.User = GetUser();
            var hotels = hotelService.GetAllHotelsOrdered();   
            return View(hotels);
        }

        public Dictionary<string,string> SearchPlaces(string key)
        {
            return commonService.SearchCities(key);
        }
        
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(User user)
        {
            
            var creationResult = userService.LogIn(user);
            if (creationResult != null)
            {

            }
            HttpContext.Session.Set("login", Encoding.ASCII.GetBytes(user.Email));
            return RedirectToAction("index");
        }
        public IActionResult Register()
        {
         
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            var creationResult = userService.Register(user);
            if (creationResult != null)
            {
              
            }
            HttpContext.Session.Set("login", Encoding.ASCII.GetBytes(user.Email));
            return RedirectToAction("index");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("login");
            return RedirectToAction("index");
        }

        private void IntitialCreate()
        {
            //con.States.Add(new State() { Id = Guid.NewGuid(), Description = "В корзине" });
            //con.States.Add(new State() { Id = Guid.NewGuid(), Description = "Оплачено" });
            //con.SaveChanges();
            //con.Roles.Add(new Role() { Id = Guid.NewGuid(), Description = "Admin" });
            //con.Roles.Add(new Role() { Id = Guid.NewGuid(), Description = "User" });
            //con.SaveChanges();

            //var canada = new Country()
            //{
            //    Id = new Guid(),
            //    Name = "Канада"
            //};
            //var usa = new Country()
            //{
            //    Id = new Guid(),
            //    Name = "США"
            //};

            //con.Countries.Add(canada);

            //con.Countries.Add(usa);

            //con.Cities.Add(new City()
            //{
            //    Id = new Guid(),
            //    Name = "Оттава",
            //    Country = canada
            //});
            //con.Cities.Add(new City()
            //{
            //    Id = new Guid(),
            //    Name = "Торонто",
            //    Country = canada
            //});
            //con.Cities.Add(new City()
            //{
            //    Id = new Guid(),
            //    Name = "Вашингтон",
            //    Country = usa
            //});
            //con.Cities.Add(new City()
            //{
            //    Id = new Guid(),
            //    Name = "Минисота",
            //    Country = usa
            //});
            //con.SaveChanges();

            //var cities = con.Cities.ToArray();
            //var description = "Мини- отель «Винтаж» открыл свои двери в июле 2010 г." +
            //   "Отель «Винтаж»-новый мини - отель бизнес класса," +
            //    "удачно сочетающий в себе лучшие традиции европейского сервиса и колорит старого Русского города." +
            //   "Отель «Винтаж»  уникален по своей архитектуре и расположению в деловом и культурном центре исторической части города," +
            //   "в начале пешеходной зоны одной из самых старинных улиц г.Калуги - ул.Театральной," +
            //     "являющейся традиционным местом отдыха калужан и гостей города," +
            //     "где в изобилии находятся кафе," +
            //     "рестораны," +
            //     "магазины," +
            //     "а по вечерам звучит живая музыка.В нескольких десятках метров от отеля находится символический центр г.Калуги - отметка «Нулевой километр».";
            //var hotel1 = new Hotel()
            //{
            //    Id = Guid.NewGuid(),
            //    Description = description,
            //    Stars = 5,
            //    Name = "Vintage",
            //    City = cities[0]
            //};
            //var hotel2 = new Hotel()
            //{
            //    Id = Guid.NewGuid(),
            //    Description = description,
            //    Stars = 5,
            //    Name = "Capital",
            //    City = cities[1]
            //};
            //var hotel3 = new Hotel()
            //{
            //    Id = Guid.NewGuid(),
            //    Description = description,
            //    Stars = 5,
            //    Name = "Luxor",
            //    City = cities[2]
            //};
            //var hotel4 = new Hotel()
            //{
            //    Id = Guid.NewGuid(),
            //    Description = description,
            //    Stars = 5,
            //    Name = "AllStars",
            //    City = cities[3]
            //};
            //con.Hotels.Add(hotel1);
            //            con.Hotels.Add(hotel2);
            //            con.Hotels.Add(hotel3);
            //            con.Hotels.Add(hotel4);
            //            con.SaveChanges();
        }
    }
}
