using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Models
{
    public class RestaurantDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactNumber { get; set; }
        public double Rating { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}