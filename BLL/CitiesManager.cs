using System;
using System.Collections.Generic;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    interface ICitiesManager
    {
        List<City> GetAll();
        City GetByID(int id);
        City Add(City city);

    }
    public class CitiesManager : ICitiesManager
    {
        public ICities_DB CitiesDbObject { get; }

        public CitiesManager(IConfiguration conf)
        {
            CitiesDbObject = new Cities_DB(conf);
        }

        public City Add(City city)
        {
            return CitiesDbObject.Add(city);
        }

        
        public List<City> GetAll()
        {
            return CitiesDbObject.GetAll();
        }


        public City GetByID(int id)
        {
            return CitiesDbObject.GetByID(id);
        }

        
    }
}
