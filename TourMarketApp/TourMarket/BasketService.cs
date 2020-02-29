using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourMarket.DAL;
using TourMarket.Entities;
using TourMarket.Interfaces;

namespace TourMarket.BLL
{
    public class BasketService : IBasketService
    {
        private readonly Context context;
        private readonly IHotelService hotelService;

        public BasketService(Context context, IHotelService hotelService)
        {
            this.context = context;
            this.hotelService = hotelService;
        }
        public bool AddToBasket(User user, Guid hotelId, DateTime dateFrom, DateTime dateTo, int countPlaces)
        {
            try
            {
                var stateName = "В корзине";
                var state = context.States.FirstOrDefault(x=>x.Description.ToLower()==stateName.ToLower());
                var sum = hotelService.GetSumOfPlaces(hotelId, dateFrom, dateTo, countPlaces);
                var hotel = context.Hotels.FirstOrDefault(x => x.Id == hotelId);
                var order = new Order()
                {
                    Id = Guid.NewGuid(),
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    CountPlaces = countPlaces,
                    State = state,
                    Hotel = hotel,
                    User=user,
                    Sum = sum
                };
                context.Orders.Add(order);
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public void Approve(Guid orderId)
        {
            var order = context.Orders.Include(x=>x.Hotel).FirstOrDefault(x => x.Id == orderId);
            var state = context.States.FirstOrDefault(x => x.Description == "Оплачено");
            for(var date = order.DateFrom; date.Date<=order.DateTo.Date; date= date.AddDays(1))
            {
                var hotelPlace = context.HotelPlaces.Include(x => x.Hotel).FirstOrDefault(x => x.Date.Date == date.Date && x.Hotel.Id == order.Hotel.Id);
                hotelPlace.CountPlaces = hotelPlace.CountPlaces - order.CountPlaces;
                context.SaveChanges();
            }
            order.State = state;
            context.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders
                .Include(x => x.User)
                .Include(x => x.Hotel).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.State)
                .OrderBy(x => x.DateFrom)
                .ToList();
        }

        public IEnumerable<Order> GetOrders(User user)
        {
            return context.Orders.Include(x => x.User)
                .Include(x=>x.Hotel).ThenInclude(x=>x.City).ThenInclude(x=>x.Country)
                .Include(x=>x.State)
                .Where(x => x.User.Id == user.Id)
                .OrderBy(x=>x.DateFrom)
                .ToList();
        }

        public void Remove(Guid orderId)
        {
            var order = context.Orders.FirstOrDefault(x => x.Id == orderId);
            context.Remove(order);
            context.SaveChanges();
        }
    }
}
