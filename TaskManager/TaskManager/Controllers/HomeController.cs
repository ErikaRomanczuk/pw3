using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private object loginRepository;

        CarpetaM carpetaModelo = new CarpetaM();
        CarpetasRepository CarpetasRepository = new CarpetasRepository();
        LoginRepository LoginRepository = new LoginRepository();
        TareaRepository TareaRepository = new TareaRepository();

        // GET: Registracion
        public ActionResult Index()
        {
            if (new UsuarioM { }.GetUser() != null)
            {
                List<Tarea> tareas = TareaRepository.TareasPriotarias(new UsuarioM { }.GetUser().IdUsuario);
                List<TareaViewModel> tareasView = tareas.Select(x => TareaViewModel.FromTarea(x))
                                                        .OrderBy(x => x.FechaFin)
                                                        .OrderByDescending(x=>x.Prioridad)
                                                        .ToList();
                return View(tareasView);
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            return View();
        }

        public ActionResult Registracion()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registracion(Usuario usuario)
        {
            return View();
        }


    }
}