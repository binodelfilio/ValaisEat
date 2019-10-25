using System;
using System.Collections.Generic;
using System.Text;
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
}
