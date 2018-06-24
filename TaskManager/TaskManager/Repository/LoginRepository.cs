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

        public bool VerificarLogin(UsuarioM usuario)
        {
            UsuarioRepository UsuarioRepository = new UsuarioRepository();

            Usuario userInBase = UsuarioRepository.buscarUsuarioPorEmailYPass(usuario);

            if (userInBase != null)
            {
                if (userInBase.Activo == 1)
                {
                    usuario = UsuarioRepository.modelarUsuario(userInBase);
                    Session["userLogged"] = usuario;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        public UsuarioM GetUser()
        {
            return (UsuarioM)Session["userLogged"];
        }
       
    }
}
