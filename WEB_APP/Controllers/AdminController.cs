using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_APP.Models;

namespace WEB_APP.Controllers
{
    /*
     * Controlleur de la partie Admin/Staff
     * Plusieurs vues utilisées 
     *  
     */


    public class AdminController : Controller
    {
        private IStaffsManager staffManager { get; set; }
        private ICustomersManager customersManager { get; set; }
        private IOrdersManager ordersManager { get; set; }
        private IOrder_DishManager order_DishManager { get; set; }
        private IDishesManager dishesManager { get; set; }


               
        public AdminController(IStaffsManager staffManager, ICustomersManager customersManager,
            IOrdersManager ordersManager,
            IDishesManager dishesManager,
            IOrder_DishManager order_DishManager)
        {
            this.staffManager = staffManager;
            this.customersManager = customersManager;
            this.ordersManager = ordersManager;
            this.order_DishManager = order_DishManager;
            this.dishesManager = dishesManager;
        }

        public IActionResult Index()
        {
            List<Panier> panier = getPaniers();
            return View(panier);
        }

        /*
         * Action qui retourne les informations du user grâce à l'ID du user transmis par session
         */
        public IActionResult Details()
        {
            var id = (int)HttpContext.Session.GetInt32("IdUser");
            
            var staff = staffManager.GetByID(id);
            return View(staff);
        }

        /*
         * Action de mise à jour des informations du staff via le bouton "sauvegarder"
         * Réaffiche la vue Detail avec les nouvelles modifications
         */
        public IActionResult Update(DTO.Staff s)
        {
            staffManager.Update(s);
            return RedirectToAction("Details");
        }
        public IActionResult Delivered(int id)
        {
            var order = ordersManager.GetByID(id);
            order.Status = DTO.Order.DELIVERED;
            order.DatetimeDelivered = DateTime.Now;
            ordersManager.Update(order);
            return RedirectToAction("Index");
        }
        private List<Panier> getPaniers()
        {
            var user = getLoggedUser();
            var orders = ordersManager.GetAllByStaff(user.IdStaff);
            List<Panier> paniers = new List<Panier>();

            foreach (var order in orders)
            {
                List<OrderDish> l_orderDish = new List<OrderDish>();
                foreach (var od in order_DishManager.GetByOrder(order.IdOrder))
                {
                    l_orderDish.Add(new OrderDish { Dish = dishesManager.GetByID(od.IdDish), Order_dish = od });
                }
                paniers.Add(new Panier { Order = order, OrderDishes = l_orderDish });
            }
            return paniers;
        }

        private DTO.Staff getLoggedUser()
        {
            var idUser = (int)HttpContext.Session.GetInt32("IdUser");
            return staffManager.GetByID(idUser);
        }
    }
}