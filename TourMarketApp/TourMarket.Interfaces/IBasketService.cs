using System;
using System.Collections.Generic;
using System.Text;
using TourMarket.Entities;

namespace TourMarket.Interfaces
{
    public interface IBasketService
    {
        bool AddToBasket(User user,Guid hotelId, DateTime dateFrom, DateTime dateTo, int countPlaces);
        IEnumerable<Order> GetOrders(User user);
        IEnumerable<Order> GetAllOrders();
        void Remove(Guid orderId);
        void Approve(Guid orderId);
    }
}
