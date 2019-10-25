using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;



namespace DAL
{
    interface IDB
    {

        IConfiguration Configuration { get; }
        List<Object> GetAll();
        Object GetByID(int id);
        void Delete(Object obj);
        Object Add(Object obj);
        Object Update(Object obj);
        // void DeleteAll();
    }
}
