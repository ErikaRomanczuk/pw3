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
        UsuarioRepository UsuarioRepository = new UsuarioRepository();

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
                Usuario usuario = UsuarioRepository.BuscarUsuarioPorEmailYPass(user);

                if (recordar != null)
                {
                    if (recordar.Contains("true"))
                    {
                        user.generarCookie();
                    }
                }
                user.guardarEnSesion(usuario);

                Dictionary<string, string> redirectTo = new LoginRepository().GetRedirectTo();
                if (redirectTo != null)
                {
                    return RedirectToAction(redirectTo["metodo"], redirectTo["controller"], null);
                }

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
            return RedirectToAction("Index","Home",null);
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
                    Usuario userInBase = UsuarioRepository.BuscarUsuarioPorEmail(user.Email);
                    if (userInBase != null)
                    {
                        if (userInBase.Activo.Equals(1))
                        {
                            ViewBag.errorMailEnUso = ErrorMailEnUso;
                        }
                        else
                        {
                            user.IdUsuario = userInBase.IdUsuario;
                            user.ActivarUsuario();

                            Usuario usuario = UsuarioRepository.BuscarUsuarioPorId(user.IdUsuario);

                            user.ModificarUsuarioEntidad(usuario);
                            UsuarioRepository.ModificarUsuario(usuario);
                            return Login(user);
                        }

                    }
                    else
                    {
                        Usuario usuario = user.ConvertirModelo();
                        UsuarioRepository.CrearUsuario(usuario);
                        return Login(user);
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