using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourMarket.DAL;
using TourMarket.Entities;
using TourMarket.Interfaces;

namespace TourMarket.BLL
{
    public class CountryService : ICountryService
    {
        Context _context;
        public CountryService(Context context)
        {
            _context = context;
        }
        public string CreateCountry(Country country)
        {
            var validation = Validation(country);
            if (!string.IsNullOrEmpty(validation)) return validation;

            country.Id = Guid.NewGuid();

            _context.Countries.Add(country);
            _context.SaveChanges();
            return null;
        }

        public string EditCountry(Country country)
        {
            var validation = Validation(country);
            if (!string.IsNullOrEmpty(validation)) return validation;
            var countryDb = _context.Countries.FirstOrDefault(x => x.Id == country.Id);
            countryDb.Name = country.Name;
            _context.SaveChanges();
            return null;
        }
        public List<Country> GetAllCountries() => _context.Countries.ToList();
        public Country GetCountry(Guid id) => _context.Countries.FirstOrDefault(x => x.Id == id);

        private string Validation(Country country)
        {
            var result = new StringBuilder();
            if (string.IsNullOrEmpty(country.Name))
                result.Append("Введите название страны");
            return result.ToString();
        }
    }
}
