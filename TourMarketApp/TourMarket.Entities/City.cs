using System;
namespace TourMarket.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
    }
}
