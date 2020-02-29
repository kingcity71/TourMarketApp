using System;
using System.Collections.Generic;
using System.Text;
using TourMarket.Entities;

namespace TourMarket.Interfaces
{
    public interface ICountryService
    {
        List<Country> GetAllCountries();
        Country GetCountry(Guid id);
        string CreateCountry(Country city);
        string EditCountry(Country city);
    }
}
