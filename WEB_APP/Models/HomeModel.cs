using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
namespace WEB_APP.Models
{
    public class HomeModel
    {
        public Login Login { get; set; }
        public DTO.Customer Customer { get; set; }
        public DTO.City City { get; set; }
    }
}
