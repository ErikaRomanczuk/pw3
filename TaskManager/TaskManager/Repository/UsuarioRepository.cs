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

        public UsuarioM modelarUsuario(Usuario usuario)
        {
            UsuarioM usuarioM = new UsuarioM();
            usuarioM.IdUsuario = usuario.IdUsuario;
            usuarioM.Nombre = usuario.Nombre;
            usuarioM.Apellido = usuario.Apellido;
            usuarioM.Email = usuario.Email;
            usuarioM.Contraseña = usuario.Contrasenia;
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
    }
}