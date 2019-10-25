using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    class Order_Dish_DB : IDB
    {
        public IConfiguration Configuration => throw new NotImplementedException();

        public object Add(object obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(object obj)
        {
            throw new NotImplementedException();
        }

        public List<object> GetAll()
        {
            throw new NotImplementedException();
        }

        public object GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public object Update(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
