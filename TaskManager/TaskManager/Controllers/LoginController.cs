using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.Repository;
//using Recaptcha;

namespace TaskManager.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            UsuarioM user = new UsuarioM();
            return View();
        }

        [HttpPost]
        public ActionResult ValidarLogin(UsuarioM user)
        {
            bool validacion = new LoginRepository().VerificarLogin(user);
 
            if (validacion)
            {
                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                return RedirectToAction("/Carpetas/Listar");
            }

        }

        public ActionResult Registracion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registracion(UsuarioM user)
        {
             RegistracionRepository registracionRepository = new RegistracionRepository();

            string EncodedResponse = Request.Form["g-Recaptcha-Response"];
            bool recaptcha = (registracionRepository.IsReCaptchValid(Request.Form["g-Recaptcha-Response"]));

            if (recaptcha)
            {
                if(registracionRepository.verificarCampoVacio(user))
                {
                    if (registracionRepository.RegistrarNewUser(user))
                    {
                        return RedirectToAction("ValidarLogin", user);
                    }
                    else
                    {
                        ViewBag.errorMailEnUso = "El E-Mail ya se encuentra en uso";
                    }
                }
            }
            ViewBag.errorCaptcha = "Captcha Incorrecto";
            return View(user);
        }
    }
}