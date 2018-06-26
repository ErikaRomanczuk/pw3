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
        private object loginRepository;

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

            //No estoy seguro si esto va acá o en el repositorio. La cosa es que acá prohíbo que me ingresen 
            //id inválida por url
            LoginRepository loginRepository = new LoginRepository();
            Carpeta carpeta = new Carpeta();
            carpeta = carpetasRepository.BuscarCarpetaPorId(idCarpeta);
            if (loginRepository.GetUser().IdUsuario != carpeta.IdUsuario)
            {
                return RedirectToAction("Listar");
            }
            carpeta = carpetasRepository.BuscarCarpetaPorId(idCarpeta);
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
            //No estoy seguro si esto va acá o en el repositorio. La cosa es que acá prohíbo que me ingresen 
            //id inválida por url
            LoginRepository loginRepository = new LoginRepository();
            Carpeta carpeta = new Carpeta();
            carpeta = carpetasRepository.BuscarCarpetaPorId(idCarpeta);
            if (loginRepository.GetUser().IdUsuario != carpeta.IdUsuario)
            {
                return RedirectToAction("Listar");
            }
            carpetasRepository.EliminarCarpeta(idCarpeta);
            return RedirectToAction("Listar");
        }

        public ActionResult Prueba()
        {
            Context ctx = new Context();
            Carpeta c = new Carpeta();
            c.Nombre = "Prueba";
            c.Descripcion = "Prueba";
            c.FechaCreacion = DateTime.Now;
            c.IdUsuario = 2;
            ctx.Carpeta.Add(c);
            ctx.SaveChanges();

            return View();
        }
    }
}