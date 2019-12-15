using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_APP.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }


        public int IdCity { get; set; }
        public String PicPath { get; set; }

        public static Restaurant Serialize(DTO.Restaurant rest)
        {
            return new Restaurant { Id = rest.IdRestaurant, Address = rest.Address, Name = rest.Name, PicPath = rest.PicPath };
        }
    }
}
