using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WEB_APP.Controllers
{
    public class PanierController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}