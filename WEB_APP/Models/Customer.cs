using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_APP.Models
{
    public class Customer
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int IdCity { get; set; }
        public static Customer Serialize(DTO.Customer customer)
        {
            return new Customer { Firstname = customer.Firstname, Lastname = customer.Lastname, Address = customer.Address, Email = customer.Email, IdCity = customer.IdCity, Username = customer.Username, Password = null };
        }

    }
}
