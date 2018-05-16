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
        public ActionResult Crear(Carpeta carpeta)
        {
            CarpetasRepository carpetasRepository = new CarpetasRepository();
            carpetasRepository.CrearCarpeta(carpeta);
            return RedirectToAction("Listar");
        }

        public ActionResult Crear()
        {
            Carpeta carpeta = new Carpeta();
            return View(carpeta);
        }

        public ActionResult Modificar(int idCarpeta)
        {
            CarpetasRepository carpetasRepository = new CarpetasRepository();
            return View(carpetasRepository.BuscarCarpetaPorId(idCarpeta));
        }

        [HttpPost]
        public ActionResult Modificar(Carpeta carpeta)
        {
            CarpetasRepository carpetasRepository = new CarpetasRepository();
            try
            {
                carpetasRepository.ModificarCarpeta(carpeta);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error al intentar guardar";
                return View("Listar", ViewBag);
            }
            return RedirectToAction("Listar");
        }
    }
}