using System;
using TourMarket.DAL;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TourMarket.Entities;
using TourMarket.Interfaces;

namespace TourMarket.BLL
{
    public class CommonService: ICommonService
    {
        private readonly Context _context;

        public CommonService(Context context)
        {
            _context = context;
        }

        public Dictionary<string,string> SearchCities (string key)
        {
            var searchEntities = _context.Cities.Include(x=>x.Country)
                .Where(x => x.Name.ToLower().Contains(key.ToLower())
                || x.Country.Name.ToLower().Contains(key.ToLower()))
                .ToList();
            var dic = new Dictionary<string, string>();
            foreach (var city in searchEntities)
                dic.Add($"{city.Country.Name}, {city.Name}", city.Id.ToString());
            return dic;
        }

        
    }
}
