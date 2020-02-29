using System;
using System.Collections.Generic;
using System.Text;
using TourMarket.Entities;

namespace TourMarket.Interfaces
{
    public interface IHotelService
    {
        bool IsPlacesExist(Guid hotelId, DateTime dateFrom, DateTime dateTo, int countPlaces);
        Hotel GetHotel(Guid id);
        string CreateHotel(Hotel hotel);
        string EditHotel(Hotel hotel);
        string CreateHotelPlace(HotelPlace hotelPlace);
        string EditHotelPlace(HotelPlace hotelPlace);
        List<Hotel> GetAllHotels();
        List<Hotel> GetAllHotelsOrdered();
        List<HotelPlace> GetHotelPlaces(Guid hotelId);
        HotelPlace GetHotelPlace(Guid hotelPlaceId);
        List<Hotel> SearchService(Guid cityId, DateTime dateFrom, DateTime dateTo);
        double GetSumOfPlaces(Guid hotelId, DateTime dateFrom, DateTime dateTo, int countPlaces);
    }
}
