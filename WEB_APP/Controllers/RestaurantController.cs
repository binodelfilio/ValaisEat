using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WEB_APP.Models;

namespace WEB_APP.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantsManager restaurantsManager { get; }
        private ICitiesManager citiesManager { get; }
        private IDishesManager dishesManager { get; }
        public RestaurantController(IDishesManager dishesManager, IRestaurantsManager restaurantsManager, ICitiesManager citiesManager)
        {
            this.restaurantsManager = restaurantsManager;
            this.citiesManager = citiesManager;
            this.dishesManager = dishesManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            if (HttpContext.Session.GetInt32("IdCustomer") == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            List <RestaurantsByCity> restaurantsByCity = new List<RestaurantsByCity>();
            ViewBag.Username = HttpContext.Session.GetString("Username");
            foreach (var city in citiesManager.GetAll())
            {
                List<Restaurant> restos = new List<Restaurant>();
                foreach (var restaurant in restaurantsManager.GetByCityId(city.IdCity))
                {
                    restos.Add(Restaurant.Serialize(restaurant));
                    restos.Add(Restaurant.Serialize(restaurant));
                    restos.Add(Restaurant.Serialize(restaurant));
                    restos.Add(Restaurant.Serialize(restaurant));
                    restos.Add(Restaurant.Serialize(restaurant));
                    restos.Add(Restaurant.Serialize(restaurant));
                    restos.Add(Restaurant.Serialize(restaurant));
                    restos.Add(Restaurant.Serialize(restaurant));
                }
                //new RestaurantsByCity { City = c, Restaurants = restos };
                restaurantsByCity.Add(new RestaurantsByCity { City = City.Serialize(city), Restaurants = restos });
               
            }
            return View(restaurantsByCity);
        }
        public IActionResult Details(int id)
        {
            var resto = Restaurant.Serialize(restaurantsManager.GetByID(id));



            List<Dish> dishes = new List<Dish>();
            foreach (var dish in dishesManager.GetByRestaurant(id))
            {
                dishes.Add(Dish.Serialize(dish));
            }
            DishesByRestaurant dishesByRestaurant = new DishesByRestaurant { Restaurant = resto, Dishes=dishes };


            return View(dishesByRestaurant);
        }
    }
}

