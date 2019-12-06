using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public interface IOrder_DishManager
    {
        List<Order_Dish> GetAll();
        Order_Dish GetByID(int id);
        int Delete(int id);
        Order_Dish Add(Order_Dish order_Dish);
    }

    class Order_DishManager : IOrder_DishManager
    {
        private IOrder_Dish_DB orderDish_db { get; }

        public Order_DishManager(IOrder_Dish_DB orderDish_db)
        {
            this.orderDish_db = orderDish_db;
        }


        public Order_Dish Add(Order_Dish order_Dish)
        {
            return orderDish_db.Add(order_Dish);
        }

        public int Delete(int id)
        {
            return orderDish_db.Delete(id);
        }

        public List<Order_Dish> GetAll()
        {
            return orderDish_db.GetAll();
        }

        public Order_Dish GetByID(int id)
        {
            return orderDish_db.GetByID(id);
        }
    }
}
