using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    interface IOrder_DishManager
    {
        List<Order_Dish> GetAll();
        Order_Dish GetByID(int id);
        int Delete(int id);
        Order_Dish Add(Order_Dish order_Dish);
    }

    class Order_DishManager : IOrder_DishManager
    {
        public IOrder_Dish_DB Order_DishDbObject { get; }

        public Order_DishManager(IConfiguration conf)
        {
            Order_DishDbObject = new Order_Dish_DB(conf);
        }


        public Order_Dish Add(Order_Dish order_Dish)
        {
            return Order_DishDbObject.Add(order_Dish);
        }

        public int Delete(int id)
        {
            return Order_DishDbObject.Delete(id);
        }

        public List<Order_Dish> GetAll()
        {
            return Order_DishDbObject.GetAll();
        }

        public Order_Dish GetByID(int id)
        {
            return Order_DishDbObject.GetByID(id);
        }
    }
}
