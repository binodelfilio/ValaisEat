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
   * Interface qui définit le comportement de la classe OrdersManager qui suit
   */
    public interface IOrdersManager
    {
        List<Order> GetAll();
        Order GetByID(int id);
        int Delete(int id);
        Order Add(Order order);
        void Update(Order order);
        Order GetCurrentOrCreate(int idCustomer);
        List<Order> GetAllByUser(int idCustomer);
        List<Order> GetAllByStaff(int idStaff);
        bool StaffHasMoreThenFive(int id, DateTime dt);

    }
    public class OrdersManager : IOrdersManager
    {
        public IOrder_DB orders_DB { get; }

        public OrdersManager(IOrder_DB orders_DB)
        {
            this.orders_DB = orders_DB;
        }
        public bool StaffHasMoreThenFive(int id, DateTime dt)
        {

            if (GetAll().Where(o => o.IdStaff == id 
            && o.Status == Order.TO_DELIVERY 
            && ((dt - o.DatetimeConfirmed).Value.TotalMinutes <= 30 && (dt - o.DatetimeConfirmed).Value.TotalMinutes >=0)).ToList().Count >= 5
                ) 
            { 
                return true;
            }
            return false;
        }

        public List<Order> GetAllByUser(int idCustomer)
        {
            List<Order> orders = new List<Order>();
            foreach (var order in GetAll().OrderByDescending(o => o.DatetimeCreated).ToList())
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
            var neworder = new Order {
                IdCustomer = idCustomer,
                IdStaff = 0,
                Status = Order.IN_PROGRESS,
                IdOrder = 0,
                NbrDish = 0,
                DatetimeCreated = DateTime.Now,
                DatetimeConfirmed = null,
                DatetimeDelivered = null
            };

            if (orders == null)
            {
                return Add(neworder);
            }
            foreach (var order in GetAll())
            {
                if(order.IdCustomer == idCustomer && order.Status == Order.IN_PROGRESS)
                {
                    return order;
                }
            }
            return Add(neworder);
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

        public List<Order> GetAllByStaff(int idStaff)
        {
            List<Order> orders = new List<Order>();
            foreach (var order in GetAll())
            {
                if (order.IdStaff== idStaff)
                    orders.Add(order);
            }
            return orders;
        }
    }
}
