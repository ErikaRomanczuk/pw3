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

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(TareaM tarea)
        {
            try
            {
                tareaRepository.crear(tarea);
                return Redirect("Listar");
            }
            catch (Exception e)
            {
                return View("error");
            }
        }

        public ActionResult Crear()
        {
            TareaM tarea = new TareaM();
            return View(tarea);
        }

        /** NOT WOWRKING 
        [HttpPost]
        public ActionResult Modificar(Tarea usuario)
        {
            return View();
        }

        public ActionResult Modificar()
        {
            return View();
        }
        **/

    }
}