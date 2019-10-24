using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    class Restaurant
    {
        public int IdRestaurant { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public City City { get; set; }
    }
}
