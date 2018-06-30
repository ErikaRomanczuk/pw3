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

        public void Logout()
        {
            Session.Abandon();
        }

        public UsuarioM GetUser()
        {
            return (UsuarioM)Session["userLogged"];
        }

        public void generarCookie()
        {
            UsuarioM user = GetUser();
            HttpCookie cookie = new HttpCookie("User");

            cookie["ID"] = user.IdUsuario.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }


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
