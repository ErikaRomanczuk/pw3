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

        CarpetasRepository CarpetasRepository = new CarpetasRepository();
        LoginRepository LoginRepository = new LoginRepository();

        // GET: Carpeta
        public ActionResult Listar()
        {
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
            CarpetasRepository.CrearCarpeta(carpetaM);
            return RedirectToAction("Listar");
        }

        public ActionResult Modificar(int idCarpeta)
        {
            //No estoy seguro si esto va acá o en el repositorio. La cosa es que acá prohíbo que me ingresen 
            //id inválida por url
            Carpeta carpeta = CarpetasRepository.BuscarCarpetaPorId(idCarpeta);
            if (LoginRepository.GetUser().IdUsuario != carpeta.IdUsuario)
            {
                return RedirectToAction("Listar");
            }
            carpeta = CarpetasRepository.BuscarCarpetaPorId(idCarpeta);
            CarpetaM carpetaM = CarpetasRepository.ModelarCarpeta(carpeta);
            return View(carpetaM);
        }
        [HttpPost]
        public ActionResult Modificar(CarpetaM carpetaM)
        {
            try
            {
                CarpetasRepository.ModificarCarpeta(carpetaM);
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
            //No estoy seguro si esto va acá o en el repositorio. La cosa es que acá prohíbo que me ingresen 
            //id inválida por url
            Carpeta carpeta = CarpetasRepository.BuscarCarpetaPorId(idCarpeta);
            if (LoginRepository.GetUser().IdUsuario != carpeta.IdUsuario)
            {
                return RedirectToAction("Listar");
            }
            CarpetasRepository.EliminarCarpeta(idCarpeta);
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