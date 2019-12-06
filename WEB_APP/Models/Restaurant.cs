using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_APP.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public string Address { get; set; }


        // TODO: Est-ce que c'est un objet ou la foreign key de l'objet ?
        // public City City { get; set; }
        public String PicPath { get; set; }
    }
}
