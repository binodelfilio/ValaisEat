using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    interface IDishesManager
    {
        List<Dish> GetAll();
        Dish GetByID(int id);
        int Delete(int id);
        Dish Add(Dish dish);
        int Update(Dish dish);
    }

    class DishesManager : IDishesManager
    {

        public IDishes_DB DishesDbObject { get; }

        public DishesManager(IConfiguration conf)
        {
            DishesDbObject = new Dishes_DB(conf);
        }


        public Dish Add(Dish dish)
        {
            return DishesDbObject.Add(dish);
        }

        public int Delete(int id)
        {
            return DishesDbObject.Delete(id);
        }

        public List<Dish> GetAll()
        {
            return DishesDbObject.GetAll();
        }

        public Dish GetByID(int id)
        {
            return DishesDbObject.GetByID(id);
        }

        public int Update(Dish dish)
        {
            return DishesDbObject.Update(dish);
        }
    }
}
