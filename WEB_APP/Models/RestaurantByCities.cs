using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WEB_APP.Models
{
    public class RestaurantsByCity
    {
        public City City { get; set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}
