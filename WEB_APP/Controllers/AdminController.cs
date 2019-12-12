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
    public class AdminController : Controller
    {
        public IStaffsManager staffManager { get; set; }
        public AdminController(IStaffsManager staffManager)
        {
            this.staffManager = staffManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            var id = (int)HttpContext.Session.GetInt32("IdUser");
            
            var staff = staffManager.GetByID(id);
            return View(staff);
        }
        public IActionResult Update(DTO.Staff s)
        {
            staffManager.Update(s);
            return RedirectToAction("Details");
        }
    }
}