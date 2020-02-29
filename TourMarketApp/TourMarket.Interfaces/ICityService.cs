using System;
using System.Collections.Generic;
using System.Text;
using TourMarket.Entities;

namespace TourMarket.Interfaces
{
    public interface ICityService
    {
        List<City> GetAllCities();
        City GetCity(Guid id);
        string CreateCity(City city);
        string EditCity(City city);
    }
}
