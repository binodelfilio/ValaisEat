using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public interface IOrdersManager
    {
        List<Order> GetAll();
        Order GetByID(int id);
        int Delete(int id);
        Order Add(Order order);
    }
    class OrdersManager : IOrdersManager
    {
        public IOrder_DB orders_DB { get; }

        public OrdersManager(IOrder_DB orders_DB)
        {
            this.orders_DB = orders_DB;
        }

        public Order Add(Order order)
        {
            return orders_DB.Add(order);
        }

        public int Delete(int id)
        {
            return orders_DB.Delete(id);
        }

        public List<Order> GetAll()
        {
            return orders_DB.GetAll();
        }

        public Order GetByID(int id)
        {
            return orders_DB.GetByID(id);
        }
    }
}
