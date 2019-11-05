using System;
using System.Collections.Generic;
using DTO;

namespace BLL
{
    interface ICitiesManager
    {
        List<City> GetAll();
        City GetByID(int id);
        void Delete(City obj);
        City Add(City obj);
        City Update(City obj);
    }
    public class CitiesManager : ICitiesManager
    {
        public City Add(City obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(City obj)
        {
            throw new NotImplementedException();
        }

        public List<City> GetAll()
        {
            throw new NotImplementedException();
        }

        public City GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public City Update(City obj)
        {
            throw new NotImplementedException();
        }
    }
}
