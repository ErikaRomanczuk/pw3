using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TareasController : Controller
    {
        // GET: Tarea
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Crear(Tarea tarea)
        {
            return View();
        }

        public ActionResult Crear()
        {
            Tarea tarea = new Tarea();
            return View(tarea);
        }


        [HttpPost]
        public ActionResult Modificar(Tarea usuario)
        {
            return View();
        }
        public ActionResult Modificar()
        {
            return View();
        }



    }
}