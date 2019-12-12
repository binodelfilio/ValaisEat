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
        public IActionResult StaffLogin(Login l)
        {
            var staff = staffsManager.GetByUsernamePassword(l.Username, l.Password);
            @TempData["ErrorLoginMessage"] = "";
            if (staff != null)
            {
                setConnectedUser(staff.IdStaff, staff.Firstname);
                return RedirectToAction("Details", "Admin");
            }
            else
            {
                setNotConnected();
                TempData["ErrorLoginMessage"] = "Username ou mot de passe incorrect";
            }
            return RedirectToAction("Index");

        }
        private void setNotConnected()
        {
            HttpContext.Session.SetString("Firstname", "");
            HttpContext.Session.SetInt32("IdUser", 0);
        }
        private void setConnectedUser(int id, string firstname)
        {
            HttpContext.Session.SetString("Firstname", firstname);
            HttpContext.Session.SetInt32("IdUser", id);
        }

    }
}
