using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourMarket.Interfaces;

namespace TourMarketApp.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketService basketService;

        public BasketController(IUserService userService, IBasketService basketService)
            :base(userService)
        {
            this.basketService = basketService;
        }
        public bool AddToBasket(Guid hotelId, DateTime dateFrom, DateTime dateTo, int countPlaces)
        {
            var user = GetUser();
            return basketService.AddToBasket(user,hotelId, dateFrom, dateTo,countPlaces);
        }
        public IActionResult Admin()
        {
            ViewBag.User = GetUser();
            var orders = basketService.GetAllOrders();
            return View(orders);
        }
        public IActionResult Index()
        {
            ViewBag.User = GetUser();
            var user = GetUser();
            var orders = basketService.GetOrders(user);
            return View(orders);
        }

        public void Remove(Guid orderId)
        {
            basketService.Remove(orderId);
        }
        public void Approve(Guid orderId)
        {
            basketService.Approve(orderId);
        }
    }
}