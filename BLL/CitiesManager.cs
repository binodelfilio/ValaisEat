using System;
using System.Collections.Generic;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public interface ICitiesManager
    {
        List<City> GetAll();
        City GetByID(int id);
        City Add(City city);
        City GetOrCreate(City city);
    }
    public class CitiesManager : ICitiesManager
    {
        private ICities_DB citie_db { get; }

        public City GetOrCreate(City city)
        {
            foreach(var c in GetAll())
            {
                if (city.Name == c.Name && city.NPA == c.NPA)
                {
                    return c;
                }
            }
            return Add(city);
        }
        public CitiesManager(ICities_DB citie_db)
        {
            this.citie_db = citie_db;
        }

        public City Add(City city)
        {
            return citie_db.Add(city);
        }

        
        public List<City> GetAll()
        {
            return citie_db.GetAll();
        }


        public City GetByID(int id)
        {
            return citie_db.GetByID(id);
        }

        
    }
}
