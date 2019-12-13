using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using WEB_APP.Models;
namespace WEB_APP.Controllers
{
    public class PanierController : Controller
    {
        private ICustomersManager customersManager { get; set; }
        private IOrdersManager ordersManager { get; set; }
        private IOrder_DishManager order_DishManager { get; set; }
        private IDishesManager dishesManager { get; set; }
        public PanierController(ICustomersManager customersManager, 
            IOrdersManager ordersManager,
            IDishesManager dishesManager,
            IOrder_DishManager order_DishManager)
        {
            this.customersManager = customersManager;
            this.ordersManager = ordersManager;
            this.order_DishManager = order_DishManager;
            this.dishesManager = dishesManager;
        }
        public IActionResult Index()
        {
            
            List<Panier> paniers = getPaniers(); 
            return View(paniers);
        }

        public IActionResult Delete(int id)
        {
            var od = order_DishManager.GetByID(id);
            var dish = dishesManager.GetByID(od.IdDish);
            var order = ordersManager.GetByID(od.IdOrder);


            order.NbrDish -= od.Quantity;
            order.TotalPrice -= (od.Quantity* dish.Price);
            HttpContext.Session.SetInt32("NbrDish", order.NbrDish);

            ordersManager.Update(order);

            order_DishManager.Delete(od.IdOrder_Dish);
            return RedirectToAction("Index");
        }

        private List<Panier> getPaniers()
        {
            var idUser = (int)HttpContext.Session.GetInt32("IdUser");
            var orders = ordersManager.GetAllByUser(idUser);
            List<Panier> paniers = new List<Panier>();

            foreach (var order in orders)
            {
                List<OrderDish> l_orderDish = new List<OrderDish>();
                foreach (var od in order_DishManager.GetByOrder(order.IdOrder))
                {
                    l_orderDish.Add(new OrderDish { Dish = dishesManager.GetByID(od.IdDish), Order_dish = od });
                }
                paniers.Add(new Panier { Order = order, OrderDishes = l_orderDish });
                // ordersDishes.Add(new OrderDish { Order = order, Dishes = dishes, OrderDishes = ods });
            }
            return paniers;
        }
        public IActionResult Confirm(int idOrder, int time)
        {
            Console.WriteLine("idOrder:" + idOrder+"; time:"+time);
            return View();
        }
    }
}