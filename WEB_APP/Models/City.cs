using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_APP.Models
{
    public class City
    {
        public string Name { get; set; }
        public string NPA { get; set; }

        public static City Serialize(DTO.City city)
        {
            return new City { Name = city.Name, NPA = city.NPA };
        }
    }
}
