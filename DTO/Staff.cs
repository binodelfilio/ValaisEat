using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Staff
    {
        public int IdStaff { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Birthdate { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public City City { get; set; }
    }
}
