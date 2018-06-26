using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class CarpetasRepository
    {
        UsuarioRepository usuarioRepository = new UsuarioRepository();
        LoginRepository loginRepository = new LoginRepository();
        Context ctx = new Context();

        public List<CarpetaM> listarCarpetasM()
        {
            List<Carpeta> listaCarpeta = new List<Carpeta>();
            listaCarpeta =  ctx.Carpeta.ToList();
            listaCarpeta = listaCarpeta.Where(x => x.IdUsuario == loginRepository.GetUser().IdUsuario).ToList();

            List<CarpetaM> listaCarpetaM = new List<CarpetaM>();
            foreach(var x in listaCarpeta)
            {
                CarpetaM carpetaM = new CarpetaM();
                carpetaM.Usuario = loginRepository.GetUser();
                carpetaM.IdCarpeta = x.IdCarpeta;
                carpetaM.Nombre = x.Nombre;
                carpetaM.Descripcion = x.Descripcion;
                carpetaM.FechaCreacion = x.FechaCreacion;
                listaCarpetaM.Add(carpetaM);

            }
            return (listaCarpetaM);
        }

        public List<CarpetaM> listarOrdenadasCarpetasM()
        {
            List<Carpeta> listaCarpeta = new List<Carpeta>();
            listaCarpeta = ctx.Carpeta
                                .OrderBy(c => c.Nombre)
                                .ToList();
            List<CarpetaM> listaCarpetaM = new List<CarpetaM>();
            foreach (var x in listaCarpeta)
            {
                CarpetaM carpetaM = new CarpetaM();
                // carpetaM.Usuario = repositoryUsuario.buscarUsuarioPorId(x.IdUsuario)
                carpetaM.IdCarpeta = x.IdCarpeta;
                carpetaM.Nombre = x.Nombre;
                carpetaM.Descripcion = x.Descripcion;
                carpetaM.FechaCreacion = x.FechaCreacion;
                listaCarpetaM.Add(carpetaM);
            }
            return (listaCarpetaM);
        }


        public void CrearCarpeta(CarpetaM carpetaM)
        {
            Carpeta carpeta = new Carpeta();
            carpeta.Descripcion = carpetaM.Descripcion;
            carpeta.Nombre = carpetaM.Nombre;
            carpeta.IdUsuario = loginRepository.GetUser().IdUsuario;
            carpeta.FechaCreacion = DateTime.Now;
            ctx.Carpeta.Add(carpeta);
            ctx.SaveChanges();
        }

        public Carpeta BuscarCarpetaPorId(int IdCarpeta)
        {
            List<Carpeta> listaCarpetas = ctx.Carpeta.ToList();
            Carpeta carpeta = listaCarpetas.Where(x => x.IdCarpeta == IdCarpeta).FirstOrDefault();
            if(carpeta == null)
            { 
                throw new Exception("Id de carpeta inexistente");
            }
            return carpeta;
        }

        public CarpetaM ModelarCarpeta (Carpeta carpeta)
        {
            CarpetaM carpetaM = new CarpetaM();
            carpetaM.IdCarpeta = carpeta.IdCarpeta;
            carpetaM.Nombre = carpeta.Nombre;
            carpetaM.Descripcion = carpeta.Descripcion;
            carpetaM.FechaCreacion = carpeta.FechaCreacion;
            //guardo el id del usuario, busco al usuario y lo modelo
            //TODO: Validar nulleable
            if (carpeta.IdUsuario != null)
            {
                int idUsuario = (int)carpeta.IdUsuario;
                Usuario usuario = usuarioRepository.buscarUsuarioPorId(idUsuario);
                carpetaM.Usuario = usuarioRepository.modelarUsuario(usuario);

            }
            else
            {
                //crearle tryCatch para validar si es usuario null
            }


            return carpetaM;
        }

        public Carpeta ConvertirModelo (CarpetaM carpetaM)
        {
            Carpeta carpeta = new Carpeta();
            carpeta.IdCarpeta = carpetaM.IdCarpeta;
            carpeta.Nombre = carpetaM.Nombre;
            carpeta.Descripcion = carpetaM.Descripcion;
            carpeta.FechaCreacion = carpetaM.FechaCreacion;
            carpeta.IdUsuario = carpetaM.Usuario.IdUsuario;

            return carpeta;
        }

        public void EliminarCarpeta (int idCarpeta)
        {
            Carpeta carpeta = BuscarCarpetaPorId(idCarpeta);
            ctx.Carpeta.Remove(carpeta);
            ctx.SaveChanges();
        }

        public void ModificarCarpeta(CarpetaM carpetaM)
        {
            Carpeta carpeta = BuscarCarpetaPorId(carpetaM.IdCarpeta);
            if (carpeta == null)
            {
                throw new ArgumentException("Carpeta con id: " + carpeta.IdCarpeta + " es inexistente");
            }

            carpeta.Nombre = carpetaM.Nombre;
            carpeta.Descripcion = carpetaM.Descripcion;
            ctx.SaveChanges();
        }

        public void CrearCarpetaGeneral (int IdUsuario)
        {
            Carpeta carpeta = new Carpeta();
            carpeta.Nombre = "General";
            carpeta.Descripcion = "Carpeta por defecto";
            carpeta.FechaCreacion = DateTime.Now;
            carpeta.IdUsuario = IdUsuario;
            ctx.Carpeta.Add(carpeta);
            ctx.SaveChanges();
        }

        public string autentificarCarpeta (int idCarpeta)
        {
            Carpeta carpeta = BuscarCarpetaPorId(idCarpeta);
            if (loginRepository.GetUser().IdUsuario != carpeta.IdUsuario)
            {
                return "Listar";
            }

            else return null;
        }
    }
}