using System;
using System.Collections.Generic;
using System.Text;

namespace TourMarket.Entities
{
    public class HotelPlace
    {
        public Guid Id { get; set; }
        public Hotel Hotel { get; set; }
        public DateTime Date { get; set; }
        public int CountPlaces { get; set; }
        public double PriceForDay { get; set; }
    }
}
