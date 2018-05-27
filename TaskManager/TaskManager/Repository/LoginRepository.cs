using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using System.Web.SessionState;

namespace TaskManager.Repository
{
    public class LoginRepository : System.Web.UI.Page //Si no hereda de esta clase no me deja usar la variable de sesion
    {
        private static string mailTest = "Admin.admin@admin.com";
        private static string passTest = "1234";

        public bool VerificarLogin(Usuario usuario)
        {
            //Deberia verificar si el mail-contraseña que se intenta loguear coincide con alguno de la tabla
            foreach (Usuario userInBase in GetUsers())
            {
                if (usuario.Email.Equals(userInBase.Email) && usuario.Contraseña.Equals(userInBase.Contraseña))
                {
                    /*Si encontramos coincidencia retornamos TRUE para que acceda al sistema y ya lo guardamos en la sesion 
                    para mas adelante poder cargar la lista de carpetas de ESE usuario*/
                    Session["userLogged"] = userInBase;
                    return true;
                }
            }
            return false;
        }

        public List<Usuario> GetUsers()
        {
            //Provisorio, aca deberia traer lista de usuarios de la base de datos 
            List<Usuario> usuarios = new List<Usuario>();

            usuarios.Add(new Usuario { Email = mailTest, Contraseña = passTest });
            return usuarios;
        }

        public bool RegistrarNewUser(Usuario user)
        {
            foreach (Usuario userInBase in GetUsers())
            {
                if (user.Email.Equals(userInBase.Email))
                {
                    if (user.Activo.Equals(1))
                    {
                        return false;
                    }
                    else if (user.Activo.Equals(0))
                    {
                        ActivarUsuario(user);
                        //Agregar metodo para cambiar nombre, apellido y contraseña de usuario desactivado
                        return true;
                    }
                    return false;
                }
            }
            ActivarUsuario(user);
            //Agregar nuevo usuario a la tabla "Usuario" de la base -- Añadir en el insert del DAO la fecha de registro

            return true;
        }

        public void ActivarUsuario(Usuario user)
        {
            DateTime LocalDate = DateTime.Now;
            string Nombre = "General";
            string Descripcion = "Carpeta por Defecto";

            user.Activo = 1;
        
            if(user.Carpetas != null)
            {
                if(user.Carpetas.Count != 0)
                {
                    foreach (Carpeta carpeta in user.Carpetas)
                    {
                        if (carpeta.Nombre.Equals(Nombre))
                        {
                            return;
                        }
                    }
                }
            }
           
            user.Carpetas.Add(new Carpeta {Nombre = Nombre, Descripcion = Descripcion, FechaCreacion = LocalDate });
        }

    }
}
