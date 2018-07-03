using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.Repository;
using TaskManager.Utils;

namespace TaskManager.Controllers
{
    public class TareaController : Controller
    {
        TareaRepository tareaRepository = new TareaRepository();
        CarpetasRepository carpetasRepository = new CarpetasRepository();
        LoginRepository loginRepository = new LoginRepository();
        DetalleTareaRepository detalleTareaRepository = new DetalleTareaRepository();
        TareaViewModel tareaModelo = new TareaViewModel();

        // GET: Tarea
        public ActionResult Index()
        {
            if (new UsuarioM { }.GetUser() == null ) {
                loginRepository.SetRedirectTo("Tarea","Index");
                return RedirectToAction("Login","Login");
            }

            string filtro = Request["filtro"];
            if (filtro != null && filtro != "")
            {
                return View( tareaRepository.ListarConFiltroCompletado(filtro).Select(x => TareaViewModel.FromTarea(x)).ToList());
            }
            else
            {
                return View(tareaRepository.ListarTodos().Select(x=> TareaViewModel.FromTarea(x)).ToList());
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
            TareaViewModel tareaM = TareaViewModel.FromTarea(tarea);
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
        public ActionResult Crear(TareaViewModel tarea)
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                loginRepository.SetRedirectTo("Tarea", "Index");
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid)
            {
                tareaRepository.Crear(TareaViewModel.ToTarea(tarea));
                return Redirect("Index");
            }

            ViewBag.carpetas = carpetasRepository.listarOrdenadasCarpetasM();
            return View(tarea);

        }

        public ActionResult Crear()
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                loginRepository.SetRedirectTo("Tarea", "Crear");
                return RedirectToAction("Login", "Login");
            }

            TareaViewModel tarea = new TareaViewModel();
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
                TareaViewModel tareaM = TareaViewModel.FromTarea(tarea);
                return View(tareaM);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Modificar(TareaViewModel tarea)
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                loginRepository.SetRedirectTo("Tarea", "Index");
                return RedirectToAction("Login", "Login");
            }

            CarpetasRepository carpetasRepository = new CarpetasRepository();
            if (ModelState.IsValid)
            {
                try
                {
                    tareaRepository.Modificar(TareaViewModel.ToTarea(tarea));
                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = "Error al intentar guardar";
                    return View("Index", ViewBag);
                }
                return RedirectToAction("Index");
            }
            return View(tarea);
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

        [HttpPost]
        public ActionResult SubirAdjunto(HttpPostedFileBase adjunto, int IdTarea)
        {
            Tarea tarea = tareaRepository.buscarPorIdTarea(IdTarea);
            if (adjunto.ContentLength > 0)
            {
                var filePath = FileUtility.Guardar(adjunto, adjunto.FileName, $"/archivos/tareas/{IdTarea}/");
                tareaRepository.AgregarArchivo(IdTarea, filePath);
                
            }
            return RedirectToAction("Detalle", new { IdTarea = IdTarea });
        }
    }
}