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
    public class UserController : Controller
    {

        private ICustomersManager customersManager { get; }

        public UserController (ICustomersManager customersManager)
        {
            this.customersManager = customersManager;
        }
        public IActionResult Index()
        {
            var id = (int)HttpContext.Session.GetInt32("IdUser");
            var customer = customersManager.GetByID(id);
            return View(customer);
        }
        [HttpPost]
        public IActionResult Save(DTO.Customer c)
        {
            // TODO: manage IdCity for modify customer's account

            customersManager.Update(c);
                               
            return RedirectToAction("Index");
        }
        public IActionResult Deconnexion()
        {
            return RedirectToAction("Index", "Home");


        }
    }
}