using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourMarket.DAL;
using TourMarket.Entities;
using TourMarket.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TourMarket.BLL
{
   public class CityService: ICityService
    {
        Context _context;
        public CityService(Context context)
        {
            _context = context;
        }

        public string CreateCity(City city)
        {
            var validation = Validation(city);
            if (!string.IsNullOrEmpty(validation)) return validation;

            city.Id = Guid.NewGuid();
            var county = _context.Countries.FirstOrDefault(x => x.Id==city.Country.Id);
            if (county == null) return "Страна не найдена";

            city.Country = county;
            _context.Cities.Add(city);
            _context.SaveChanges();
            return null;
        }

        public string EditCity(City city)
        {
            var validation = Validation(city);
            if (!string.IsNullOrEmpty(validation)) return validation;

            var county = _context.Countries.FirstOrDefault(x => x.Id == city.Country.Id);
            if (county == null) return "Страна не найдена";

            city.Country = county;
            var cityDB = _context.Cities.FirstOrDefault(x => x.Id == city.Id);
            cityDB.Name = city.Name;
            cityDB.Country = city.Country;
            _context.SaveChanges();
            return null;
        }

        public List<City> GetAllCities()
            => _context.Cities.Include(x => x.Country).ToList();
        

        public City GetCity(Guid id) => _context.Cities.Where(x => x.Id == id).Include(x => x.Country).FirstOrDefault();

        private string Validation(City city)
        {
            var result = new StringBuilder();
            if (string.IsNullOrEmpty(city.Name))
                result.Append("Введите название города");
            if(city.Country?.Id==Guid.Empty)
                result.Append("Укажите страну");
            return result.ToString();
        }
    }
}
