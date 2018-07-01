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
        TareaM tareaModelo = new TareaM();

        // GET: Tarea
        public ActionResult Index()
        {
            if (new UsuarioM { }.GetUser() == null ) {
                loginRepository.SetRedirectTo("Tarea","Index");
                return RedirectToAction("Login","Login");
            }

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
            if (new UsuarioM { }.GetUser() == null)
            {
                loginRepository.SetRedirectTo("Tarea", "Index");
                return RedirectToAction("Login", "Login");
            }

            Tarea tarea = tareaRepository.buscarPorIdTarea(IdTarea);
            if(tarea.IdUsuario != new UsuarioM { }.GetUser().IdUsuario)
            {
                return RedirectToAction("Index");
            }
            TareaM tareaM = tareaModelo.ModelarTarea(tarea);
            ViewBag.ListaComentarioTareaM = detalleTareaRepository.Listar(IdTarea);
            return View(tareaM);
        }

        public ActionResult CrearComentarioTarea (int IdTarea)
        {

            if (new UsuarioM { }.GetUser() == null)
            {
                loginRepository.SetRedirectTo("Tarea", "Index");
                return RedirectToAction("Login", "Login");
            }

            Tarea tarea = tareaRepository.buscarPorIdTarea(IdTarea);
            if (tarea.IdUsuario != new UsuarioM { }.GetUser().IdUsuario)
            {
                return RedirectToAction("Index");
            }
            ComentarioTareaM comentarioTareaM = new ComentarioTareaM();
            ViewBag.IdTarea = IdTarea;
            return View(comentarioTareaM);
        }

        [HttpPost]
        public ActionResult CrearComentarioTareaM(ComentarioTareaM comentarioTareaM)
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                loginRepository.SetRedirectTo("Tarea", "Index");
                return RedirectToAction("Login", "Login");
            }

            string IdTarea = Request["IdTarea"];
            detalleTareaRepository.Crear(comentarioTareaM, IdTarea);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int IdTarea)
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                loginRepository.SetRedirectTo("Tarea", "Index");
                return RedirectToAction("Login", "Login");
            }

            Tarea tarea = new Tarea();
            tarea = tareaRepository.buscarPorIdTarea((IdTarea));
            if (new UsuarioM { }.GetUser().IdUsuario == tarea.IdUsuario)
            {
                tareaRepository.Borrar(IdTarea);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Crear(TareaM tarea)
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                loginRepository.SetRedirectTo("Tarea", "Index");
                return RedirectToAction("Login", "Login");
            }

            String idCarpeta = Request["Carpeta"];
            tareaRepository.Crear(tarea, idCarpeta);
            return Redirect("Index");
        }

        public ActionResult Crear()
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                loginRepository.SetRedirectTo("Tarea", "Crear");
                return RedirectToAction("Login", "Login");
            }

            TareaM tarea = new TareaM();
            ViewBag.carpetas = carpetasRepository.listarOrdenadasCarpetasM();
            return View(tarea);
        }


        public ActionResult Modificar(int idTarea)
        {

            if (new UsuarioM { }.GetUser() == null)
            {
                loginRepository.SetRedirectTo("Tarea", "Index");
                return RedirectToAction("Login", "Login");
            }

            ViewBag.carpetas = carpetasRepository.listarOrdenadasCarpetasM();
            Tarea tarea = tareaRepository.buscarPorIdTarea(idTarea);
            if (tarea == null)
            {
                throw new Exception("Id de carpeta inexistente : " + idTarea);
            }
            if (new UsuarioM { }.GetUser().IdUsuario == tarea.IdUsuario)
            {
                TareaM tareaM = tareaModelo.ModelarTarea(tarea);
                return View(tareaM);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Modificar(TareaM tarea)
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                loginRepository.SetRedirectTo("Tarea", "Index");
                return RedirectToAction("Login", "Login");
            }

            CarpetasRepository carpetasRepository = new CarpetasRepository();
            String idCarpeta = Request["Carpeta"];
            try
            {
                tareaRepository.Modificar(tarea, idCarpeta);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error al intentar guardar";
                return View("Index", ViewBag);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Completar(int IdTarea)
        {

            if (new UsuarioM { }.GetUser() == null)
            {
                loginRepository.SetRedirectTo("Tarea", "Index");
                return RedirectToAction("Login", "Login");
            }

            Tarea tarea = new Tarea();
            tarea = tareaRepository.buscarPorIdTarea(IdTarea);
            if (new UsuarioM { }.GetUser().IdUsuario == tarea.IdUsuario)
            {
                tareaRepository.CompletarPorIdTarea(IdTarea);
            }
            return RedirectToAction("Index");
        }
    }
}