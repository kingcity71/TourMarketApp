using System;
using System.Collections.Generic;
using TourMarket.Entities;

namespace TourMarket.Interfaces
{
    public interface ICommonService
    {
        Dictionary<string, string> SearchCities(string key);
        
    }
}
