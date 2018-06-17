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
            return View(CarpetasRepository.listarCarpetasM());
        }

        public ActionResult Crear()
        {
            CarpetaM carpetaM = new CarpetaM();
            return View(carpetaM);
        }

        [HttpPost]
        public ActionResult Crear(CarpetaM carpetaM)
        {
            CarpetasRepository carpetasRepository = new CarpetasRepository();
            carpetasRepository.CrearCarpeta(carpetaM);
            return RedirectToAction("Listar");
        }

        public ActionResult Modificar(int idCarpeta)
        {
            CarpetasRepository carpetasRepository = new CarpetasRepository();
            Carpeta carpeta = carpetasRepository.BuscarCarpetaPorId(idCarpeta);
            CarpetaM carpetaM = carpetasRepository.ModelarCarpeta(carpeta);
            return View(carpetaM);
        }
        [HttpPost]
        public ActionResult Modificar(CarpetaM carpetaM)
        {
            CarpetasRepository carpetasRepository = new CarpetasRepository();
            try
            {
                carpetasRepository.ModificarCarpeta(carpetaM);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error al intentar guardar";
                return View("Listar", ViewBag);
            }
            return RedirectToAction("Listar");
        }

        public ActionResult Eliminar (int idCarpeta)
        {
            CarpetasRepository carpetasRepository = new CarpetasRepository();
            carpetasRepository.EliminarCarpeta(idCarpeta);
            return RedirectToAction("Listar");
        }
    }
}