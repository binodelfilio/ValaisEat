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
        private ICustomersManager customersManager { get; set; }
        public HomeController(ICustomersManager customersManager)
        {
            this.customersManager = customersManager;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login l)
        {
            var customer = customersManager.GetByUsernamePassword(l.Username, l.Password);
            @TempData["ErrorLoginMessage"] = "";
            if (customer != null)
            {
                HttpContext.Session.SetString("Firstname", customer.Firstname);
                HttpContext.Session.SetInt32("IdCustomer", customer.IdCustomer);
                return RedirectToAction("List", "Restaurant");
            } else
            {
                HttpContext.Session.SetString("Username", "");
                HttpContext.Session.SetInt32("IdCustomer", 0);
                HttpContext.Session.SetString("Connected", "");
                TempData["ErrorLoginMessage"] = "Username ou mot de passe incorrect";
            }
            return RedirectToAction("Index");

        }
    }
}
