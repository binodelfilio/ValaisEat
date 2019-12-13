using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Order_Dish
    {
        public int IdOrder_Dish { get; set; }
        public int Quantity { get; set; }
        public int IdDish { get; set; }
        public int IdOrder { get; set; }

    }
}
