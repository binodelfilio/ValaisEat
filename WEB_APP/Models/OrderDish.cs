using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_APP.Models
{
    public class OrderDish
    {
        public DTO.Order_Dish Order_dish { get; set; }
        public DTO.Dish Dish { get; set; }
    }
}
