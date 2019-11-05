using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    interface IOrdersManager
    {
        List<Order> GetAll();
        Order GetByID(int id);
        void Delete(Order obj);
        Order Add(Order obj);
        Order Update(Order obj);
    }
    class OrdersManager : IOrdersManager
    {
        public Order Add(Order obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order obj)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order obj)
        {
            throw new NotImplementedException();
        }
    }
}
