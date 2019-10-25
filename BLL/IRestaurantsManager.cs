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
}
