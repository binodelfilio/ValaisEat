﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Order_Dish
    {
        public int IdOrder_Dish { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
        public Dish Dish { get; set; }
        public Order Order { get; set; }

    }
}