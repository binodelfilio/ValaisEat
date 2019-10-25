using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Order
    {
        public int IdOrder { get; set; }
        public string Status { get; set; }
        public Customer Customer { get; set; }
        public Staff Staff { get; set; }
    }
}
