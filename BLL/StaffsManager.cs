using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    interface IStaffsManager
    {
        List<Staff> GetAll();
        Staff GetByID(int id);
        int Delete(int id);
        Staff Add(Staff staff);
        int Update(Staff staff);
    }

    class StaffsManager : IStaffsManager
    {
        public IStaffs_DB StaffsDbObject { get; }

        public StaffsManager(IConfiguration conf)
        {
            StaffsDbObject = new Staffs_DB(conf);
        }

        public Staff Add(Staff staff)
        {
            return StaffsDbObject.Add(staff);
        }

        public int Delete(int id)
        {
            return StaffsDbObject.Delete(id);
        }

        public List<Staff> GetAll()
        {
            return StaffsDbObject.GetAll();
        }

        public Staff GetByID(int id)
        {
            return StaffsDbObject.GetByID(id);
        }

        public int Update(Staff staff)
        {
            return StaffsDbObject.Update(staff);
        }
    }
}
