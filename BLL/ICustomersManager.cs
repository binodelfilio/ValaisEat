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
}
