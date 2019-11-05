using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    interface IDishesManager
    {
        List<Dish> GetAll();
        Dish GetByID(int id);
        void Delete(Dish obj);
        Dish Add(Dish obj);
        Dish Update(Dish obj);
    }

    class DishesManager : IDishesManager
    {
        public Dish Add(Dish obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Dish obj)
        {
            throw new NotImplementedException();
        }

        public List<Dish> GetAll()
        {
            throw new NotImplementedException();
        }

        public Dish GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Dish Update(Dish obj)
        {
            throw new NotImplementedException();
        }
    }
}
