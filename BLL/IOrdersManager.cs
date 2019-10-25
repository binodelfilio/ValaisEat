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
}
