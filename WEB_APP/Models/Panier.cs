using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_APP.Models;
namespace WEB_APP.Models
{
    public class Panier
    {
        public DTO.Staff Staff { get; set; }
        public DTO.Order Order { get; set; }
        public List<OrderDish> OrderDishes { get; set; }
    }
}
