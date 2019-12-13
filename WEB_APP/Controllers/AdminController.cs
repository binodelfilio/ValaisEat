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
        public IStaffsManager staffManager { get; set; }
        public AdminController(IStaffsManager staffManager)
        {
            this.staffManager = staffManager;
        }

        public IActionResult Index()
        {
            return View();
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
    }
}