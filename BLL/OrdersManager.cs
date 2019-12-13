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
        void Update(Order order);
        Order GetCurrentOrCreate(int idCustomer);
        List<Order> GetAllByUser(int idCustomer);
    }
    public class OrdersManager : IOrdersManager
    {
        public IOrder_DB orders_DB { get; }

        public OrdersManager(IOrder_DB orders_DB)
        {
            this.orders_DB = orders_DB;
        }
        
        public List<Order> GetAllByUser(int idCustomer)
        {
            List<Order> orders = new List<Order>();
            foreach (var order in GetAll())
            {
                if (order.IdCustomer == idCustomer)
                    orders.Add(order);
            }
            return orders;
        }
        public void Update(Order order)
        {
            orders_DB.Update(order);
        }
        public Order GetCurrentOrCreate(int idCustomer)
        {
            var orders = GetAll();
            if (orders == null)
            {
                return Add(new Order
                {
                    IdCustomer = idCustomer,
                    IdStaff = 0,
                    Status = Order.IN_PROGRESS,
                    IdOrder = 0,
                    NbrDish = 0,
                    DatetimeCreated = DateTime.Now,
                    DatetimeConfirmed = null,
                    DatetimeDelivered = null
                });
            }
            foreach (var order in GetAll())
            {
                if(order.IdCustomer == idCustomer && order.Status == Order.IN_PROGRESS)
                {
                    return order;
                }
            }
            return null;
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
