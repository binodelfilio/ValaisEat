using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BLL;
using DTO;

namespace WEB_APP.Controllers
{
    /* 
     * Controlleur de la partie User/Client (customer)
     * Plusieurs vues utilisées 
     */

    public class UserController : Controller
    {

        private ICustomersManager customersManager { get; }

        public UserController (ICustomersManager customersManager)
        {
            this.customersManager = customersManager;
        }

        /*
         * Action qui retourne les informations du user grâce à l'ID du user transmis par session
         */
        public IActionResult Index()
        {
            var id = (int)HttpContext.Session.GetInt32("IdUser");
            var customer = customersManager.GetByID(id);
            return View(customer);
        }

        /*
         * Action de mise à jour des informations du customer via le bouton "sauvegarder"
         * Réaffiche la vue Index avec les nouvelles modifications
         */
        [HttpPost]
        public IActionResult Save(DTO.Customer c)
        {
            // TODO: manage IdCity for modify customer's account

            customersManager.Update(c);
                               
            return RedirectToAction("Index");
        }

        /*
         * Action de déconnexion
         */
        public IActionResult Deconnexion()
        {
            return RedirectToAction("Index", "Home");


        }
    }
}