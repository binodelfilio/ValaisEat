using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    interface IOrder_DishManager
    {
        List<Order_Dish> GetAll();
        Order_Dish GetByID(int id);
        void Delete(Order_Dish obj);
        Order_Dish Add(Order_Dish obj);
        Order_Dish Update(Order_Dish obj);
    }
}
