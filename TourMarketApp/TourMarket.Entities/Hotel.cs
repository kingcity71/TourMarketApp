using System;
using System.Collections.Generic;
using System.Text;

namespace TourMarket.Entities
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
        public int Stars { get; set; }
        public string Description { get; set; }
    }
}
