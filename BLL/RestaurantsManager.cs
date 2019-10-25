using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    interface IRestaurantsManager
    {
        List<Restaurant> GetAll();
        Restaurant GetByID(int id);
        void Delete(Restaurant obj);
        Restaurant Add(Restaurant obj);
        Restaurant Update(Restaurant obj);
    }
    class RestaurantsManager : IRestaurantsManager
    {
        Restaurant IRestaurantsManager.Add(Restaurant obj)
        {
            throw new NotImplementedException();
        }

        void IRestaurantsManager.Delete(Restaurant obj)
        {
            throw new NotImplementedException();
        }

        List<Restaurant> IRestaurantsManager.GetAll()
        {
            throw new NotImplementedException();
        }

        Restaurant IRestaurantsManager.GetByID(int id)
        {
            throw new NotImplementedException();
        }

        Restaurant IRestaurantsManager.Update(Restaurant obj)
        {
            throw new NotImplementedException();
        }
    }
}
