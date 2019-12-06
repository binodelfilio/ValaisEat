using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public interface IDishesManager
    {
        List<Dish> GetAll();
        Dish GetByID(int id);
        int Delete(int id);
        Dish Add(Dish dish);
        int Update(Dish dish);
    }

    class DishesManager : IDishesManager
    {

        private IDishes_DB dishes_db { get; }

        public DishesManager(IDishes_DB dishes_db)
        {
            this.dishes_db = dishes_db;
        }


        public Dish Add(Dish dish)
        {
            return dishes_db.Add(dish);
        }

        public int Delete(int id)
        {
            return dishes_db.Delete(id);
        }

        public List<Dish> GetAll()
        {
            return dishes_db.GetAll();
        }

        public Dish GetByID(int id)
        {
            return dishes_db.GetByID(id);
        }

        public int Update(Dish dish)
        {
            return dishes_db.Update(dish);
        }
    }
}
