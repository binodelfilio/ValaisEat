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

    class StaffsManager : IStaffsManager
    {
        public Staff Add(Staff obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Staff obj)
        {
            throw new NotImplementedException();
        }

        public List<Staff> GetAll()
        {
            throw new NotImplementedException();
        }

        public Staff GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Staff Update(Staff obj)
        {
            throw new NotImplementedException();
        }
    }
}
