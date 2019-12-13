using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{

    /*
   * Interface qui définit le comportement de la classe CustomersManager qui suit
   */
    public interface ICustomersManager
    {
        List<Customer> GetAll();
        Customer GetByID(int id);
        Customer Add(Customer customer);
        int Update(Customer customer);
        Customer GetByUsernamePassword(string username, string password);
    }

    public class CustomersManager : ICustomersManager
    {
        public ICustomers_DB customers_db { get; }

        public CustomersManager(ICustomers_DB customers_db)
        {
            this.customers_db = customers_db;
        }

        public Customer Add(Customer customer)
        {
            return customers_db.Add(customer);
        }

        public List<Customer> GetAll()
        {
            return customers_db.GetAll();
        }

        public Customer GetByID(int id)
        {
            return customers_db.GetByID(id);
        }

        public int Update(Customer customer)
        {
            return customers_db.Update(customer);
        }
        public Customer GetByUsernamePassword(string username, string password)
        {
            
            return customers_db.GetByUsernamePassword(username, password);
        }
    }
}
