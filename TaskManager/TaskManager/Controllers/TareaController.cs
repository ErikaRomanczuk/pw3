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
        // GET: Tarea
        public ActionResult Listar()
        {
            TareaRepository tareaResitory = new TareaRepository();
            return View(tareaResitory.listarTodos());
        }

        public ActionResult detalle(int id)
        {
            try { 
            TareaRepository tareaRepository = new TareaRepository();
                return View( tareaRepository.buscarPorIdTarea(id) );
            }catch(Exception e)
            {
                return View("error");
            }
        }
        public ActionResult Delete()
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