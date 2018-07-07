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

        CarpetaM carpetaModelo = new CarpetaM();
        CarpetasRepository CarpetasRepository = new CarpetasRepository();
        LoginRepository LoginRepository = new LoginRepository();
        TareaRepository tareaRepository = new TareaRepository();

        // GET: Carpeta
        public ActionResult Index()
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                LoginRepository.SetRedirectTo("Carpetas", "Index",null);
                return RedirectToAction("Login", "Login");
            }

            return View(CarpetasRepository.listarCarpetasM());
        }

        public ActionResult Crear()
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                LoginRepository.SetRedirectTo("Carpetas", "Crear",null);
                return RedirectToAction("Login", "Login");
            }

            CarpetaM carpetaM = new CarpetaM();
            return View(carpetaM);
        }

        [HttpPost]
        public ActionResult Crear(CarpetaM carpetaM)
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                LoginRepository.SetRedirectTo("Carpetas", "Crear",null);
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                CarpetasRepository.CrearCarpeta(carpetaM);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int idCarpeta)
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                LoginRepository.SetRedirectTo("Carpetas", "Index",null);
                return RedirectToAction("Login", "Login");
            }

            Carpeta carpeta = CarpetasRepository.BuscarCarpetaPorId(idCarpeta);
            if (new UsuarioM { }.GetUser().IdUsuario != carpeta.IdUsuario)
            {
                return RedirectToAction("Index");
            }

            carpeta = CarpetasRepository.BuscarCarpetaPorId(idCarpeta);
            CarpetaM carpetaM = carpetaModelo.ModelarCarpeta(carpeta);
            return View(carpetaM);
        }

        [HttpPost]
        public ActionResult Modificar(CarpetaM carpetaM)
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                LoginRepository.SetRedirectTo("Carpetas", "Index",null);
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    CarpetasRepository.ModificarCarpeta(carpetaM);
                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = "Error al intentar guardar";
                    return View("Index", ViewBag);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar (int idCarpeta)
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                LoginRepository.SetRedirectTo("Carpetas", "Index",null);
                return RedirectToAction("Login", "Login");
            }

            Carpeta carpeta = CarpetasRepository.BuscarCarpetaPorId(idCarpeta);
            if (new UsuarioM { }.GetUser().IdUsuario != carpeta.IdUsuario)
            {
                return RedirectToAction("Index");
            }
            CarpetasRepository.EliminarCarpeta(idCarpeta);
            return RedirectToAction("Index");
        }


        public ActionResult Tareas(int IdCarpeta)
        {
            if (new UsuarioM { }.GetUser() == null)
            {
                LoginRepository.SetRedirectTo("Carpetas", "Index",null);
                return RedirectToAction("Login", "Login");
            }
            List<TareaViewModel> tareaM = tareaRepository.ListarTareasDeCarpeta(IdCarpeta).Select(x => TareaViewModel.FromTarea(x)).ToList();
            return View(tareaM);
        }
    }
}