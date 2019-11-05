using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    interface IOrdersManager
    {
        List<Order> GetAll();
        Order GetByID(int id);
        int Delete(int id);
        Order Add(Order order);
    }
    class OrdersManager : IOrdersManager
    {
        public IOrder_DB OrdersDbObject { get; }

        public OrdersManager(IConfiguration conf)
        {
            OrdersDbObject = new Orders_DB(conf);
        }

        public Order Add(Order order)
        {
            return OrdersDbObject.Add(order);
        }

        public int Delete(int id)
        {
            return OrdersDbObject.Delete(id);
        }

        public List<Order> GetAll()
        {
            return OrdersDbObject.GetAll();
        }

        public Order GetByID(int id)
        {
            return OrdersDbObject.GetByID(id);
        }
    }
}
