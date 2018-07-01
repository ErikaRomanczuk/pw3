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
        public string VerificarLogin(UsuarioM usuario)
        {
            Usuario userInBase = UsuarioRepository.BuscarUsuarioPorEmailYPass(usuario);

            if (userInBase != null)
            {
                if (userInBase.Activo == 1)
                {
                    usuario = UsuarioRepository.ModelarUsuario(userInBase);
                    Session["userLogged"] = usuario;
                    return "OK";
                }
                else return "Usuario Inactivo";
            }
            else return "Datos de Login Incorrectos";
        }


        /// <summary>
        /// Cierra y vacia la Sesion actual
        /// </summary>
        public void Logout()
        {
            Session.Abandon();
        }

        /// <summary>
        /// Metodo que retorna el Usuario Logueado Almacenado en la Sesion
        /// </summary>
        /// <returns>El Modelo del usuario en sesion</returns>
        public UsuarioM GetUser()
        {
            return (UsuarioM)Session["userLogged"];
        }


        /// <summary>
        /// Metodo que Crea y setea un cookie en el sistema con el ID del usuario logueado
        /// El tiempo de expiracion de la cookie creada es 1 dia
        /// </summary>
        public void generarCookie()
        {
            UsuarioM user = GetUser();
            HttpCookie cookie = new HttpCookie("User");

            cookie["ID"] = user.IdUsuario.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Lee la cookie creada con el ID del usuario 
        /// </summary>
        /// <returns>id del usuario</returns>
        public UsuarioM getCookie()
        {
            try
            {
                HttpCookie Cookie = HttpContext.Current.Request.Cookies.Get("User");
                var id = Cookie["ID"];

                if (id != null)
                {
                    if (id != String.Empty)
                    {
                        int idUsuario = int.Parse(id);

                        UsuarioM usuarioM = UsuarioRepository.ModelarUsuario(UsuarioRepository.BuscarUsuarioPorId(idUsuario));

                        if (usuarioM != null) return usuarioM;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
