using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_APP.Models
{
    public class DishesByRestaurant
    {
        
        public Restaurant Restaurant { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}
