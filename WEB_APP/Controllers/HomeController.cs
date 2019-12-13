using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB_APP.Models;
using Microsoft.AspNetCore.Http;
using BLL;

namespace WEB_APP.Controllers
{
    public class HomeController : Controller
    {
        /*
         * Controlleur de la partie Home (Première vue de l'application web)
         */
        private ICustomersManager customersManager { get; set; }
        private IStaffsManager staffsManager { get; set; }
        public HomeController(ICustomersManager customersManager, IStaffsManager staffsManager)
        {
            this.customersManager = customersManager;
            this.staffsManager = staffsManager;
        }
        public IActionResult Index()
        {
            setNotConnected();
            return View();
        }


        /*
         * Contrôle du username et password pour la connexion pour le CUSTOMER
         * Si ok, affiche la vue List de la partie customer (liste des restaurants à choix)
         * Si faux, message d'erreur
         */

        [HttpPost]
        public IActionResult Login(Login l)
        {
            var customer = customersManager.GetByUsernamePassword(l.Username, l.Password);
            @TempData["ErrorLoginMessage"] = "";
            if (customer != null)
            {
                setConnectedUser(customer.IdCustomer, customer.Firstname);
                return RedirectToAction("List", "Restaurant");
            } else
            {
                setNotConnected();
                TempData["ErrorLoginMessage"] = "Username ou mot de passe incorrect";
            }
            return RedirectToAction("Index");

        }

        /*
         * Contrôle du username et password pour la connexion pour le STAFF
         * Si ok, affiche la vue Index de la partie admin
         * Si faux, message d'erreur
         */
        public IActionResult StaffLogin(Login l)
        {
            var staff = staffsManager.GetByUsernamePassword(l.Username, l.Password);
            @TempData["ErrorLoginMessage"] = "";
            if (staff != null)
            {
                setConnectedUser(staff.IdStaff, staff.Firstname);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                setNotConnected();
                TempData["ErrorLoginMessage"] = "Username ou mot de passe incorrect";
            }
            return RedirectToAction("Index");

        }

        /*
         * Méthode qui retourne des informations nulles de connexion à la session
         */
        private void setNotConnected()
        {
            HttpContext.Session.SetString("Firstname", "");
            HttpContext.Session.SetInt32("IdUser", 0);
        }

        /*
         * Méthode qui transmet à la session le firstname et l'id 
         */
        private void setConnectedUser(int id, string firstname)
        {
            HttpContext.Session.SetString("Firstname", firstname);
            HttpContext.Session.SetInt32("IdUser", id);
        }

    }
}
