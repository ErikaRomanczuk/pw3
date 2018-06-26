using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    public class TareaController : Controller
    {
        TareaRepository tareaRepository = new TareaRepository();

        // GET: Tarea
        public ActionResult Listar()
        {
            return View(tareaRepository.listarTodos());
        }

        public ActionResult detalle(int id)
        {
            return View(tareaRepository.buscarPorIdTarea(id));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            tareaRepository.Borrar(id);
            return RedirectToAction("Listar");
        }

        [HttpPost]
        public ActionResult Crear(TareaM tarea)
        {
            String idCarpeta = Request["Carpeta"];
            tareaRepository.Crear(tarea, idCarpeta);
            return Redirect("Listar");
        }

        public ActionResult Crear()
        {
            TareaM tarea = new TareaM();
            CarpetasRepository carpetasRepository = new CarpetasRepository();
            ViewBag.carpetas = carpetasRepository.listarOrdenadasCarpetasM();
            return View(tarea);
        }

        
        public ActionResult Modificar(int idTarea)
        {
            Tarea tarea = tareaRepository.buscarPorIdTarea(idTarea);
            TareaM tareaM = tareaRepository.ModelarTarea(tarea);
            return View(tareaM);
        }

        [HttpPost]
        public ActionResult Modificar(Tarea usuario)
        {
            return View();
        }

    }
}