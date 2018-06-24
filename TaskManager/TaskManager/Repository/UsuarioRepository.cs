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
        public Usuario buscarUsuarioPorId(int IdUsuario)
        {
            List<Usuario> listaUsuarios = ctx.Usuario.ToList();
            Usuario usuario = listaUsuarios.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
            return usuario;
        }

        public Usuario buscarUsuarioPorEmail(string email)
        {
            List<Usuario> listaUsuarios = ctx.Usuario.ToList();
            Usuario usuario = listaUsuarios.Where(x => x.Email == email).FirstOrDefault();
            return usuario;
        }

        public Usuario buscarUsuarioPorEmailYPass(UsuarioM usr)
        {
            List<Usuario> listaUsuarios = ctx.Usuario.ToList();
            Usuario usuario = listaUsuarios.Where(x => x.Email == usr.Email && x.Contrasenia == usr.Contrasena).FirstOrDefault();
            return usuario;
        }

        public void modificarUsuario(UsuarioM usuarioModel)
        {
            Usuario usuario = buscarUsuarioPorId(usuarioModel.IdUsuario);

            if (usuario != null)
            {
                usuario = convertirModelo(usuarioModel);
                ctx.SaveChanges();
            }

        }

        public Usuario convertirModelo(UsuarioM usuarioModel)
        {
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

            return usuario;
        }


        public void crearUsuario(UsuarioM usuarioModel)
        {
            Usuario usuario = convertirModelo(usuarioModel);
            DateTime localDate = DateTime.Now;

            usuario.FechaRegistracion = localDate;
            ctx.Usuario.Add(usuario);
            ctx.SaveChanges();
        }

        public UsuarioM modelarUsuario(Usuario usuario)
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

            if (usuario != null)
            {
                return true;
            }
            return false;
        }

       
    }
}