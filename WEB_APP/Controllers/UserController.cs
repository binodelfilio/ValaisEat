using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WEB_APP.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Deconnexion()
        {
            HttpContext.Session.SetString("Username", "");
            HttpContext.Session.SetInt32("IdCustomer", 0);
            HttpContext.Session.SetString("Connected", "");
            return RedirectToAction("Index", "Home");


        }
    }
}