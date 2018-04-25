using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        // GET: Registracion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            return View();
        }

        public ActionResult Registracion()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registracion(Usuario usuario)
        {
            return View();
        }


    }
}