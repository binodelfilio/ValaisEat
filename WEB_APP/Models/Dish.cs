using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_APP.Models
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

        public String PicPath { get; set; }

        public static Dish Serialize(DTO.Dish dish)
        {
            return new Dish { IdDish = dish.IdDish, Name = dish.Name, Price = dish.Price, TimePrepa = dish.TimePrepa, PicPath = dish.PicPath };
        }
    }
}
