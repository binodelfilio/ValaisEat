using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Dish
    {
        public int IdDish { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        /*
         * TimePrepa => Unit: minute
         */
        public int TimePrepa { get; set; }
        public int IdRestaurant { get; set; }
        public String PicPath { get; set; }
    }
}
