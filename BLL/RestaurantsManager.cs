using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    /*
   * Interface qui définit le comportement de la classe RestaurantsManager qui suit
   */
    public interface IRestaurantsManager
    {
        List<Restaurant> GetAll();
        Restaurant GetByID(int id);
        int Delete(int id);
        Restaurant Add(Restaurant restaurant);
        List<Restaurant> GetByCityId(int idCity);
        int Update(Restaurant restaurant);
    }
    public class RestaurantsManager : IRestaurantsManager
    {

        public IRestaurants_DB restaurants_DB { get; }

        public RestaurantsManager(IRestaurants_DB restaurants_DB)
        {
            this.restaurants_DB = restaurants_DB;
        }

        public List<Restaurant> GetByCityId(int idCity)
        {
            
            return GetAll().Where(r => r.IdCity == idCity).ToList();
            
        }
        public Restaurant Add(Restaurant restaurant)
        {
            return restaurants_DB.Add(restaurant);
        }

        public int Delete(int id)
        {
            return restaurants_DB.Delete(id);
        }

        public List<Restaurant> GetAll()
        {
            return restaurants_DB.GetAll();
        }

        public Restaurant GetByID(int id)
        {
            return restaurants_DB.GetByID(id);
        }

        public int Update(Restaurant restaurant)
        {
            return restaurants_DB.Update(restaurant);
        }
    }
}
