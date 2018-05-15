using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    public class CarpetasController : Controller
    {
        // GET: Carpeta
        public ActionResult Listar()
        {
            CarpetasRepository CarpetasRepository = new CarpetasRepository();
            return View(CarpetasRepository.listarCarpetas());
        }


        [HttpPost]
        public ActionResult Crear(Carpeta tarea)
        {
            return View();
        }

        public ActionResult Crear()
        {
            Carpeta carpeta = new Carpeta();
            return View(carpeta);
        }


        [HttpPost]
        public ActionResult Modificar(Carpeta usuario)
        {
            return View();
        }
        public ActionResult Modificar()
        {
            return View();
        }
    }
}