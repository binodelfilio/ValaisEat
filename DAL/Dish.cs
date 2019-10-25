using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Dish
    {
        public int IdDish { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        /*
         * TimePrepa => Unit: minute
         */
        public string TimePrepa { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
