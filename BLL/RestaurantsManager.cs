using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public interface IRestaurantsManager
    {
        List<Restaurant> GetAll();
        Restaurant GetByID(int id);
        int Delete(int id);
        Restaurant Add(Restaurant restaurant);
        int Update(Restaurant restaurant);
    }
    public class RestaurantsManager : IRestaurantsManager
    {

        public IRestaurants_DB RestaurantsDbObject { get; }

        public RestaurantsManager(IConfiguration conf)
        {
            RestaurantsDbObject = new Restaurants_DB(conf);
        }


        public Restaurant Add(Restaurant restaurant)
        {
            return RestaurantsDbObject.Add(restaurant);
        }

        public int Delete(int id)
        {
            return RestaurantsDbObject.Delete(id);
        }

        public List<Restaurant> GetAll()
        {
            return RestaurantsDbObject.GetAll();
        }

        public Restaurant GetByID(int id)
        {
            return RestaurantsDbObject.GetByID(id);
        }

        public int Update(Restaurant restaurant)
        {
            return RestaurantsDbObject.Update(restaurant);
        }
    }
}
