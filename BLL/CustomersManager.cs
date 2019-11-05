using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    interface ICustomersManager
    {
        List<Customer> GetAll();
        Customer GetByID(int id);
        Customer Add(Customer customer);
        int Update(Customer customer);
    }

    class CustomersManager : ICustomersManager
    {
        public ICustomers_DB CustomersDbObject { get; }

        public CustomersManager(IConfiguration conf)
        {
            CustomersDbObject = new Customers_DB(conf);
        }

        public Customer Add(Customer customer)
        {
            return CustomersDbObject.Add(customer);
        }

        public List<Customer> GetAll()
        {
            return CustomersDbObject.GetAll();
        }

        public Customer GetByID(int id)
        {
            return CustomersDbObject.GetByID(id);
        }

        public int Update(Customer customer)
        {
            return CustomersDbObject.Update(customer);
        }
    }
}
