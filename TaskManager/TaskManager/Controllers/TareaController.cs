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
        CarpetasRepository carpetasRepository = new CarpetasRepository();
        LoginRepository loginRepository = new LoginRepository();
        DetalleTareaRepository detalleTareaRepository = new DetalleTareaRepository();

        // GET: Tarea
        public ActionResult Listar()
        {
            String filtro = Request["filtro"];
            if (filtro != null && filtro != "")
            {
                return View(tareaRepository.listarConFiltroCompletado(filtro));
            }
            else
            {
                return View(tareaRepository.listarTodos());
            }

        }

        public ActionResult Detalle(int IdTarea)
        {
            Tarea tarea = tareaRepository.buscarPorIdTarea(IdTarea);
            if(tarea.IdUsuario != loginRepository.GetUser().IdUsuario)
            {
                return RedirectToAction("Listar");
            }
            TareaM tareaM = tareaRepository.ModelarTarea(tarea);
            ViewBag.ListaComentarioTareaM = detalleTareaRepository.Listar(IdTarea);
            return View(tareaM);
        }

        public ActionResult CrearComentarioTarea (int IdTarea)
        {
            Tarea tarea = tareaRepository.buscarPorIdTarea(IdTarea);
            if (tarea.IdUsuario != loginRepository.GetUser().IdUsuario)
            {
                return RedirectToAction("Listar");
            }
            ComentarioTareaM comentarioTareaM = new ComentarioTareaM();
            ViewBag.IdTarea = IdTarea;
            return View(comentarioTareaM);
        }

        [HttpPost]
        public ActionResult CrearComentarioTareaM(ComentarioTareaM comentarioTareaM)
        {
            string IdTarea = Request["IdTarea"];
            detalleTareaRepository.Crear(comentarioTareaM, IdTarea);
            return RedirectToAction("Listar");
        }

        public ActionResult Eliminar(int IdTarea)
        {
            Tarea tarea = new Tarea();
            tarea = tareaRepository.buscarPorIdTarea((IdTarea));
            if (loginRepository.GetUser().IdUsuario == tarea.IdUsuario)
            {
                tareaRepository.Borrar(IdTarea);
            }
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
            ViewBag.carpetas = carpetasRepository.listarOrdenadasCarpetasM();
            return View(tarea);
        }


        public ActionResult Modificar(int idTarea)
        {
            ViewBag.carpetas = carpetasRepository.listarOrdenadasCarpetasM();
            Tarea tarea = tareaRepository.buscarPorIdTarea(idTarea);
            if (tarea == null)
            {
                throw new Exception("Id de carpeta inexistente : " + idTarea);
            }
            if (loginRepository.GetUser().IdUsuario == tarea.IdUsuario)
            {
                TareaM tareaM = tareaRepository.ModelarTarea(tarea);
                return View(tareaM);
            }
            return RedirectToAction("Listar");
        }

        [HttpPost]
        public ActionResult Modificar(TareaM tarea)
        {
            CarpetasRepository carpetasRepository = new CarpetasRepository();
            String idCarpeta = Request["Carpeta"];
            try
            {
                tareaRepository.Modificar(tarea, idCarpeta);
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