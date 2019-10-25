using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    interface IStaffsManager
    {
        List<Staff> GetAll();
        Staff GetByID(int id);
        void Delete(Staff obj);
        Staff Add(Staff obj);
        Staff Update(Staff obj);
    }
}
