using System;
using System.Collections.Generic;
using System.Text;

namespace TourMarket.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public State State { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Hotel Hotel { get; set; }
        public double Sum { get; set; }
        public int CountPlaces { get; set; }
    }
}
