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
        private ICitiesManager citiesManager { get; set; }
        public HomeController(ICustomersManager customersManager, IStaffsManager staffsManager, ICitiesManager citiesManager)
        {
            this.customersManager = customersManager;
            this.staffsManager = staffsManager;
            this.citiesManager = citiesManager;
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
        public IActionResult Login(HomeModel hm)
        {
            var l = hm.Login;
            var customer = customersManager.GetByUsernamePassword(l.Username, l.Password);
            @TempData["ErrorLoginMessage"] = "";
            if (customer != null)
            {
                setCustConnected(customer.IdCustomer, customer.Firstname);
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
        public IActionResult StaffLogin(HomeModel hm)
        {
            var l = hm.Login;
            var staff = staffsManager.GetByUsernamePassword(l.Username, l.Password);
            @TempData["ErrorLoginMessage"] = "";
            if (staff != null)
            {
                setStaffConnected(staff.IdStaff, staff.Firstname);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                setNotConnected();
                TempData["ErrorLoginMessage"] = "Username ou mot de passe incorrect";
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult CreateLogin(HomeModel homeModel)
        {
            var city = citiesManager.GetOrCreate(homeModel.City);
            homeModel.Customer.IdCity = city.IdCity;
            var curtomer = customersManager.Add(homeModel.Customer);
            if (curtomer == null)
            {
                TempData["ErrorLoginMessage"] = "Email ou username déjà existant";
            }
            return RedirectToAction("Index");
        }


        /*
         * Méthode qui retourne des informations nulles de connexion à la session
         */
        private void setNotConnected()
        {
            HttpContext.Session.SetString("Firstname", "");
            HttpContext.Session.SetInt32("IdStaff", 0);
            HttpContext.Session.SetInt32("IdCustomer", 0);
        }

        /*
         * Méthode qui transmet à la session le firstname et l'id 
         */
        private void setCustConnected(int id, string firstname)
        {
            HttpContext.Session.SetString("Firstname", firstname);
            HttpContext.Session.SetInt32("IdCustomer", id);
        }
        private void setStaffConnected(int id, string firstname)
        {
            HttpContext.Session.SetString("Firstname", firstname);
            HttpContext.Session.SetInt32("IdStaff", id);
        }
        
    }
}
