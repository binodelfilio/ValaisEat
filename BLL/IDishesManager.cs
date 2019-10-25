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
}
