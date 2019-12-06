using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using WEB_APP.Models;

namespace WEB_APP.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurants_DB restaurant_db { get; }
        private List<Restaurant> Restos { get; set; }
        public RestaurantController(IRestaurants_DB restaurant_db)
        {
            this.restaurant_db = restaurant_db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            return View(Restos);
        }
    }
}
