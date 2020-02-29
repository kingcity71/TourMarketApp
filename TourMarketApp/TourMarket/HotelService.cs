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
    public class HotelService : IHotelService
    {
        Context _context;
        public HotelService(Context context)
        {
            _context = context;
        }

        public List<Hotel> SearchService(Guid cityId, DateTime dateFrom, DateTime dateTo)
        {
            var hotels = _context.Hotels.Include(x=>x.City).ThenInclude(x=>x.Country).Where(x => x.City.Id == cityId).ToList();

            if (dateFrom == DateTime.MinValue && dateTo == DateTime.MinValue) return hotels;

            var hotelPlaces = _context.HotelPlaces
                    .Include(x => x.Hotel)
                    .Where(x => hotels.Select(z => z.Id).ToList().Contains(x.Hotel.Id)).ToList();

            if (dateTo == DateTime.MinValue)
            {
                var targetPlaces = hotelPlaces.Where(x => x.Date == dateFrom).Select(x => x.Hotel.Id).ToList();
                return hotels.Where(x => targetPlaces.Contains(x.Id)).ToList();
            }

            hotelPlaces = hotelPlaces.OrderByDescending(x => x.Date).ToList();

            var targetHotels = new List<Hotel>();

            foreach(var hotel in hotels)
            {
                var isExist = true;
                foreach(var hotelPlace in hotelPlaces)
                {
                    if(!(hotelPlace.Date.Date>=dateFrom && hotelPlace.Date.Date <= dateTo))
                    {
                        isExist = false;
                        break;
                    }
                }
                if (isExist)
                    targetHotels.Add(hotel);
            }
            return targetHotels;
        }

        public string CreateHotel(Hotel hotel)
        {
            var validation = Validation(hotel);
            if (validation != null) return validation;

            var cityDb = _context.Cities.FirstOrDefault(x => x.Id == hotel.City.Id);
            if (cityDb == null) return "Укажите город";

            hotel.Id = Guid.NewGuid();
            hotel.City = cityDb;
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
            return null;
        }

        public string EditHotel(Hotel hotel)
        {
            var validation = Validation(hotel);
            if (validation != null) return validation;

            var cityDb = _context.Cities.FirstOrDefault(x => x.Id == hotel.City.Id);
            if (cityDb == null) return "Укажите город";

            var hotelDB = _context.Hotels.FirstOrDefault(x => x.Id == hotel.Id);
            hotelDB.City = cityDb;
            hotelDB.Name = hotel.Name;
            hotelDB.Stars = hotel.Stars;
            hotelDB.Description = hotel.Description;
            _context.SaveChanges();
            return null;
        }

        public List<Hotel> GetAllHotels() => _context.Hotels.Include(x => x.City).ThenInclude(x => x.Country).ToList();

        public Hotel GetHotel(Guid id) => _context.Hotels.Where(x => x.Id == id).Include(x => x.City).ThenInclude(x => x.Country).FirstOrDefault();

        public List<HotelPlace> GetHotelPlaces(Guid hotelId) =>
        _context.HotelPlaces.Include(x => x.Hotel).Where(x => x.Hotel.Id == hotelId)
            .OrderByDescending(x => x.Date)
            .ToList();

        public HotelPlace GetHotelPlace(Guid hotelPlaceId) =>
        _context.HotelPlaces
            .Include(x => x.Hotel)
            .Where(x => x.Id == hotelPlaceId)
            .FirstOrDefault();

        public List<Hotel> GetAllHotelsOrdered()
            => _context.Hotels
            .OrderByDescending(x => x.Stars)
            .Include(x => x.City)
            .ThenInclude(x => x.Country).ToList();

        private string Validation(Hotel hotel)
        {
            if (string.IsNullOrEmpty(hotel.Name))
                return "Укажите название";
            if (hotel?.City?.Id == Guid.Empty)
                return "Укажите город";
            if (string.IsNullOrEmpty(hotel.Description))
                return "Укажите описание";
            if (hotel?.Stars < 1 || hotel?.Stars > 5)
                return "Укажите кол-во звезд от 1 до 5";
            return null;
        }

        public string CreateHotelPlace(HotelPlace hotelPlace)
        {
            var dateDB = _context.HotelPlaces.Include(x => x.Hotel).Where(x => x.Hotel.Id == hotelPlace.Hotel.Id && x.Date == hotelPlace.Date).FirstOrDefault();
            if (dateDB != null) return "Указаная дата уже существует";
            if (hotelPlace.Date < new DateTime(2010, 1, 1))
                return "Введите корректную дату";
            var valid = Validation(hotelPlace);
            if (valid != null) return valid;

            var hotel = _context.Hotels.FirstOrDefault(x => x.Id == hotelPlace.Hotel.Id);

            hotelPlace.Id = Guid.NewGuid();
            hotelPlace.Hotel = hotel;
            _context.HotelPlaces.Add(hotelPlace);
            _context.SaveChanges();
            return null;
        }

        public string EditHotelPlace(HotelPlace hotelPlace)
        {
            var valid = Validation(hotelPlace);
            if (valid != null) return valid;

            var hotelPlaceDb = _context.HotelPlaces.Include(x => x.Hotel).Where(x => x.Id == hotelPlace.Id).FirstOrDefault();
            hotelPlaceDb.CountPlaces = hotelPlace.CountPlaces;
            hotelPlaceDb.PriceForDay = hotelPlace.PriceForDay;
            _context.SaveChanges();
            return null;
        }
        private string Validation(HotelPlace hotelPlace)
        {

            if (hotelPlace.PriceForDay < 0)
                return "Введите корректную стоимость";
            if (hotelPlace.CountPlaces < 0)
                return "Введите корректное число дней";
            return null;
        }

        public bool IsPlacesExist(Guid hotelId, DateTime dateFrom, DateTime dateTo, int countPlaces)
        {
            var hotel = _context.Hotels.FirstOrDefault(x => x.Id == hotelId);
            if (hotel == null) return false;
            for(var i=dateFrom; i<=dateTo; i = i.AddDays(1))
            {
                var hotelPlace = _context.HotelPlaces.FirstOrDefault(x => x.Date.Date == i.Date);
                if (hotelPlace == null) return false;
                if (hotelPlace.CountPlaces < countPlaces) return false;
            }
            return true;
        }

        public double GetSumOfPlaces(Guid hotelId, DateTime dateFrom, DateTime dateTo, int countPlaces)
        {
            if (!IsPlacesExist(hotelId, dateFrom, dateTo, countPlaces)) return -1;
            var sum = 0.0;
            for (var i = dateFrom; i <= dateTo; i = i.AddDays(1))
            {
               var place = _context.HotelPlaces.FirstOrDefault(x => x.Date.Date == i.Date);
                sum += (place.PriceForDay * countPlaces);
            }
            return sum;
        }
    }
}
