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
            List<Usuario> listaUsuarios = ctx.Usuario.ToList();
            Usuario usuario = listaUsuarios.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
            return usuario;
        }

        /// <summary>
        /// Busca el usuario por E-Mail en el contexto
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Usuario</returns>
        public Usuario BuscarUsuarioPorEmail(string email)
        {
            List<Usuario> listaUsuarios = ctx.Usuario.ToList();
            Usuario usuario = listaUsuarios.Where(x => x.Email == email).FirstOrDefault();
            return usuario;
        }

        /// <summary>
        /// Busca el usuario por E-Mail y Contraseña en el Contexto
        /// </summary>
        /// <param name="usr"></param>
        /// <returns>Usuario</returns>
        public Usuario BuscarUsuarioPorEmailYPass(UsuarioM usr)
        {
            List<Usuario> listaUsuarios = ctx.Usuario.ToList();
            Usuario usuario = listaUsuarios.Where(x => x.Email == usr.Email && x.Contrasenia == usr.Contrasena).FirstOrDefault();
            return usuario;
        }


        /// <summary>
        /// Modifica el usuario en el Contexto
        /// </summary>
        /// <param name="usuarioModel">Modelo de usuario a Modificar</param>
        public void ModificarUsuario(UsuarioM usuarioModel)
        {
            Usuario usuario = BuscarUsuarioPorId(usuarioModel.IdUsuario);

            if (usuario != null)
            {
                usuario = ConvertirModelo(usuarioModel);
                ctx.SaveChanges();
            }

        }

        public Usuario ConvertirModelo(UsuarioM usuarioModel)
        {
            CarpetasRepository carpetasRepository = new CarpetasRepository();
            Usuario usuario = new Usuario();

            usuario.IdUsuario = usuarioModel.IdUsuario;
            usuario.Activo = (Int16)usuarioModel.Activo;
            usuario.Apellido = usuarioModel.Apellido;
            usuario.Nombre = usuarioModel.Nombre;
            usuario.Contrasenia = usuarioModel.Contrasena;
            usuario.Email = usuarioModel.Email;
            usuario.FechaActivacion = (DateTime?)usuarioModel.FechaActivacion;
            usuario.FechaRegistracion = usuarioModel.FechaRegistracion;
            usuario.CodigoActivacion = usuarioModel.CodigoActivacion;

            foreach (CarpetaM carpetaModel in usuarioModel.Carpetas)
            {
                usuario.Carpeta.Add(carpetasRepository.ConvertirModelo(carpetaModel));
            }
            return usuario;
        }


        public void CrearUsuario(UsuarioM usuarioModel)
        {
            Usuario usuario = ConvertirModelo(usuarioModel);
            DateTime localDate = DateTime.Now;

            usuario.FechaRegistracion = localDate;
            ctx.Usuario.Add(usuario);
            ctx.SaveChanges();
        }

        public UsuarioM ModelarUsuario(Usuario usuario)
        {
            UsuarioM usuarioM = new UsuarioM();
            usuarioM.IdUsuario = usuario.IdUsuario;
            usuarioM.Nombre = usuario.Nombre;
            usuarioM.Apellido = usuario.Apellido;
            usuarioM.Email = usuario.Email;
            usuarioM.Contrasena = usuario.Contrasenia;
            usuarioM.Activo = usuario.Activo;
            if (usuario.FechaActivacion != null)
            {
                usuarioM.FechaActivacion = (DateTime)usuario.FechaActivacion;
            }
            else
            {
                DateTime myDate = DateTime.ParseExact("1900-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                usuarioM.FechaActivacion = myDate;
            }

            usuarioM.FechaRegistracion = usuario.FechaRegistracion;
            usuarioM.CodigoActivacion = usuario.CodigoActivacion;

            return usuarioM;
        }

        public bool codigoActivacionExiste(string codigo)
        {
            List<Usuario> listaUsuarios = ctx.Usuario.ToList();
            Usuario usuario = listaUsuarios.Where(x => x.CodigoActivacion == codigo).FirstOrDefault();

            if (usuario != null) return true;

            return false;
        }


    }
}