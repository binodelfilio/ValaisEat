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
        private IOrder_DishManager order_DishManager { get; }
        private IOrdersManager ordersManager { get; }
        public RestaurantController(IDishesManager dishesManager, 
            IOrdersManager ordersManager,
            IOrder_DishManager order_DishManager,
            IRestaurantsManager restaurantsManager, 
            ICitiesManager citiesManager)
        {
            this.restaurantsManager = restaurantsManager;
            this.citiesManager = citiesManager;
            this.dishesManager = dishesManager;
            this.ordersManager = ordersManager;
            this.order_DishManager = order_DishManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        /*
         * Vue qui affiche la liste des restaurants au customer
         * Si il arrive sur cette vue sans se connecter, il est automatiquement renvoyer à l'index de la home
         */
        public IActionResult List()
        {
            if (HttpContext.Session.GetInt32("IdCustomer") == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            getCurrentPanier();
            List <RestaurantsByCity> restaurantsByCity = new List<RestaurantsByCity>();
            ViewBag.Username = HttpContext.Session.GetString("Username");
            foreach (var city in citiesManager.GetAll())
            {
                List<Restaurant> restos = new List<Restaurant>();
                foreach (var restaurant in restaurantsManager.GetByCityId(city.IdCity))
                {
                    restos.Add(Restaurant.Serialize(restaurant));
                  
                }
                //new RestaurantsByCity { City = c, Restaurants = restos };
                restaurantsByCity.Add(new RestaurantsByCity { City = City.Serialize(city), Restaurants = restos });
               
            }
            return View(restaurantsByCity);
        }

        /*
         * Lorsque le customer clique sur le restaurant, une vue détaillée s'affiche
         * Vue qui liste les repas selon un restaurant donné
         */

        public IActionResult Details(int id)
        {
            var rest = restaurantsManager.GetByID(id);
            if(rest == null)
            {
                return RedirectToAction("List");
            }

            List<Dish> dishes = new List<Dish>();
            foreach (var dish in dishesManager.GetByRestaurant(id))
            {
                dishes.Add(Dish.Serialize(dish));
            }
            DishesByRestaurant dishesByRestaurant = new DishesByRestaurant { Restaurant = Restaurant.Serialize(rest), Dishes=dishes };
            return View(dishesByRestaurant);
        }
        public IActionResult AddDishToOrder(int idDish, int idResto)
        {
            var order = getCurrentPanier();
            order.NbrDish += 1;
            var timeprepa = dishesManager.GetByID(idDish).TimePrepa;
            if (order.TimeToPrepare > timeprepa)
                order.TimeToPrepare = timeprepa;
            order.TotalPrice += dishesManager.GetByID(idDish).Price;
            HttpContext.Session.SetInt32("NbrDish", order.NbrDish);
            ordersManager.Update(order);
            order_DishManager.GetOrCreate(new DTO.Order_Dish { IdDish = idDish, IdOrder = order.IdOrder, IdOrder_Dish = 0, Quantity = 1 });
            return RedirectToAction("Details", new { id = idResto });
        }

        // Un controller peut-il avoir un autre controller en paramètre
        private DTO.Order getCurrentPanier()
        {
            var userId = (int)HttpContext.Session.GetInt32("IdCustomer");
            var order = ordersManager.GetCurrentOrCreate(userId);
            HttpContext.Session.SetInt32("NbrDish", order.NbrDish);

            return order;
        }
    }
}

