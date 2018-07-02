using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class UsuarioRepository
    {
        Context ctx = new Context();

        /// <summary>
        /// Busca el usuario por ID en el contexto
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns>Usuario</returns>
        public Usuario BuscarUsuarioPorId(int IdUsuario)
        {

            Usuario usuario = ctx.Usuario.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
            return usuario;
        }

        /// <summary>
        /// Busca el usuario por E-Mail en el contexto
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Usuario</returns>
        public Usuario BuscarUsuarioPorEmail(string email)
        {
            Usuario usuario = ctx.Usuario.Where(x => x.Email == email).FirstOrDefault();
            return usuario;
        }

        /// <summary>
        /// Busca el usuario por E-Mail y Contraseña en el Contexto
        /// </summary>
        /// <param name="usr"></param>
        /// <returns>Usuario</returns>
        public Usuario BuscarUsuarioPorEmailYPass(UsuarioM usr)
        {
            Usuario usuario = ctx.Usuario.Where(x => x.Email == usr.Email && x.Contrasenia == usr.Contrasena).FirstOrDefault();
            return usuario;
        }


        /// <summary>
        /// Modifica el usuario en el Contexto
        /// </summary>
        /// <param name="usuario">Usuario a Modificar</param>
        public void ModificarUsuario(Usuario usuario)
        {
            ctx.SaveChanges();
        }


        public void CrearUsuario(Usuario usuario)
        {
            DateTime localDate = DateTime.Now;

            usuario.FechaRegistracion = localDate;
            ctx.Usuario.Add(usuario);
            ctx.SaveChanges();
        }

        public bool codigoActivacionExiste(string codigo)
        {

            Usuario usuario = ctx.Usuario.Where(x => x.CodigoActivacion == codigo).FirstOrDefault();

            if (usuario != null)
            {
                return true;
            }

            return false;
        }


    }
}