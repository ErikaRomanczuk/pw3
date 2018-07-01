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

        string ErrorMailEnUso = "El E-Mail ya se encuentra en uso";
        string ErrorCaptchaIncorrecto = "Captcha Incorrecto";
        RegistracionRepository RegistracionRepository = new RegistracionRepository();

        public ActionResult Login()
        {
            UsuarioM usuarioCookie = new UsuarioM().getCookie();

            if (usuarioCookie != null)
            {
                return Login(usuarioCookie);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsuarioM user)
        {

            string validacion = user.VerificarLogin();
            var recordar = Request.Form["recordarUsuario"];

            if (validacion == "OK")
            {
                if (recordar != null)
                {
                    if (recordar.Contains("true"))
                    {
                        user.generarCookie();
                    }
                }

                // TO DO: Ver porque no funciona
                //Dictionary<string,string> redirectTo = LoginRepository.GetRedirectTo();
                //if ( redirectTo != null)
                //{
                //    return RedirectToAction(redirectTo["metodo"], redirectTo["controller"], null);
                //}

                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                ViewBag.errorLogin = validacion;
            }
            return View("Login", user);

        }

        public ActionResult Registracion()
        {
            return View();
        }

        public ActionResult Logout()
        {
            new UsuarioM { }.Logout();
            return View("Index");
        }

        [HttpPost]
        public ActionResult Registracion(UsuarioM user)
        {

            string EncodedResponse = Request.Form["g-Recaptcha-Response"];
            bool recaptcha = (RegistracionRepository.IsReCaptchValid(Request.Form["g-Recaptcha-Response"]));

            if (recaptcha)
            {
                if (ModelState.IsValid)
                {
                    if (user.Registrar())
                    {
                        return Login(user);
                    }
                    else
                    {
                        ViewBag.errorMailEnUso = ErrorMailEnUso;
                    }
                }
            }
            else
            {
                ViewBag.errorCaptcha = ErrorCaptchaIncorrecto;
            }
            return View(user);
        }
    }
}