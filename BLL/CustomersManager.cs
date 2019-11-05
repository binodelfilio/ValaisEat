using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    interface ICustomersManager
    {
        List<Customer> GetAll();
        Customer GetByID(int id);
        void Delete(Customer obj);
        Customer Add(Customer obj);
        Customer Update(Customer obj);
    }

    class CustomersManager : ICustomersManager
    {
        public Customer Add(Customer obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer obj)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Customer Update(Customer obj)
        {
            throw new NotImplementedException();
        }
    }
}
