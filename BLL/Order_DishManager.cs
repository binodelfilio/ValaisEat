using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    /*
   * Interface qui définit le comportement de la classe Order_DishManager qui suit
   */
    public interface IOrder_DishManager
    {
        List<Order_Dish> GetAll();
        Order_Dish GetByID(int id);
        int Delete(int id);
        int Update(Order_Dish order_Dish);
        Order_Dish Add(Order_Dish order_Dish);
        Order_Dish GetOrCreate(Order_Dish order_Dish);
        List<Order_Dish> GetByOrder(int idOrder);
        int GetTimePrepa(int idOrder);

    }

    public class Order_DishManager : IOrder_DishManager
    {
        private IOrder_Dish_DB orderDish_db { get; }

        public Order_DishManager(IOrder_Dish_DB orderDish_db)
        {
            this.orderDish_db = orderDish_db;
        }
        public int GetTimePrepa(int idOrder)
        {
            return 0;
        }
        public int Update(Order_Dish order_Dish)
        {
            return orderDish_db.Update(order_Dish);
        }
        public Order_Dish GetOrCreate(Order_Dish order_Dish)
        {
            var all = GetAll();
            if (all==null)
                return Add(order_Dish);
            foreach (var od in GetAll())
            {
                if (order_Dish.IdOrder == od.IdOrder && order_Dish.IdDish == od.IdDish)
                {
                    od.Quantity += 1;
                    Update(od);
                    return od;
                }
            }
            return Add(order_Dish);
        }
        public Order_Dish Add(Order_Dish order_Dish)
        {
            return orderDish_db.Add(order_Dish);
        }
        public List<Order_Dish> GetByOrder(int idOrder)
        {
            List<Order_Dish> ods = new List<Order_Dish>();
            var all = GetAll();
            if (all == null)
                return ods;
            foreach (var od in GetAll())
            {
                if (od.IdOrder == idOrder)
                    ods.Add(od);
            }
            return ods;
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
