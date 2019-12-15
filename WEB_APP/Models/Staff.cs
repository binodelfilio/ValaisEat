using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_APP.Models
{
    public class Staff
    {
        public int IdStaff { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Birthdate { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public int IdCity { get; set; }

        public static Staff Serialize(DTO.Staff staff)
        {
            return new Staff
            {
                IdStaff = staff.IdStaff,
                Firstname = staff.Firstname,
                Lastname = staff.Lastname,
                Birthdate = staff.Birthdate,
                Address = staff.Address,
                Username = staff.Username,
                IdCity = staff.IdCity
            };
        }
    }
    

}
