using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Microsoft.AspNetCore.Http;
using WEB_APP.Models;


namespace WEB_APP.Controllers
{
    public class DishController : Controller
    {
        private IRestaurantsManager restaurantsManager { get; }

        private IDishesManager dishesManager { get; }


        public DishController(IRestaurantsManager restaurantsManager, IDishesManager dishesManager)
        {
            this.restaurantsManager = restaurantsManager;
            this.dishesManager = dishesManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
    }
}