using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using System.Web.SessionState;

namespace TaskManager.Repository
{
    public class LoginRepository : System.Web.UI.Page
    {
        UsuarioRepository UsuarioRepository = new UsuarioRepository();

        /// <summary>
        /// Metodo para Validar Login que recibe un Modelo de usuario con el Email y Contraseña
        /// validan que el mismo exista en la base, este activado 
        /// En caso de cumplir con estas validaciones genera la variable de sesion del mismo para que quede logueado en el sistema.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns> Un mensaje ya sea de error o que la validacion salio OK </returns>
        public string VerificarLogin(Usuario usuario)
        {
            if (usuario != null)
            {
                if (usuario.Activo == 1)
                {
                    return "OK";
                }
                else return "Usuario Inactivo";
            }
            else return "Datos de Login Incorrectos";
        }


        public void SetRedirectTo(string controller, string metodo)
        {
            HttpCookie cookie = new HttpCookie("redirect");
            cookie["controller"] = controller;
            cookie["metodo"] = metodo;
            cookie.Expires = DateTime.Now.AddMinutes(2);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            return;
        }

        public Dictionary<string, string> GetRedirectTo()
        {
            HttpCookie Cookie = HttpContext.Current.Request.Cookies.Get("redirect");
            if (Cookie != null)
            {
                var controller = Cookie["controller"];
                var metodo = Cookie["metodo"];
                if (controller != null && metodo != null)
                {
                    var dictionary = new Dictionary<string, string>();
                    dictionary.Add("controller", controller);
                    dictionary.Add("metodo", metodo);
                    return dictionary;
                }
            }

            return null;
        }
    }
}
