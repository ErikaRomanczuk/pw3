using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidarLogin(Usuario user)
        {
            bool validacion = new LoginRepository().VerificarLogin(user);

            if (validacion)
            {
                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
   
        public ActionResult Registracion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registracion(Usuario user)
        {
            if(new LoginRepository().RegistrarNewUser(user))
            {
              return RedirectToAction("ValidarLogin", user);
            }
            else
            {
                // Aca iria el return de la vista "amigable" de que ya existe un usuario activo con ese mail
            }
            return RedirectToAction("Login");
        }
    }
}