using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class CarpetasRepository
    {
        CarpetaM carpetaModelo = new CarpetaM();
        UsuarioRepository usuarioRepository = new UsuarioRepository();
        LoginRepository loginRepository = new LoginRepository();
        Context ctx = new Context();

        public List<CarpetaM> listarCarpetasM()
        {
            UsuarioM usuarioM = new UsuarioM().GetUser();
            List<Carpeta> listaCarpeta = new List<Carpeta>();
            listaCarpeta =  ctx.Carpeta.ToList();
            listaCarpeta = listaCarpeta.Where(x => x.IdUsuario == usuarioM.IdUsuario)
                                       .OrderBy(x=> x.Nombre)
                                       .ToList();
            List<CarpetaM> listaCarpetaM = new List<CarpetaM>();
            foreach(var x in listaCarpeta)
            {
                CarpetaM carpetaM = carpetaModelo.ModelarCarpeta(x);
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
                CarpetaM carpetaM = carpetaModelo.ModelarCarpeta(x);
                listaCarpetaM.Add(carpetaM);
            }
            return (listaCarpetaM);
        }


        public void CrearCarpeta(CarpetaM carpetaM)
        {
            Carpeta carpeta = new Carpeta();
            carpeta.Descripcion = carpetaM.Descripcion;
            carpeta.Nombre = carpetaM.Nombre;
            carpeta.IdUsuario = new UsuarioM { }.GetUser().IdUsuario;
            carpeta.FechaCreacion = DateTime.Now;
            ctx.Carpeta.Add(carpeta);
            ctx.SaveChanges();
        }

        public Carpeta BuscarCarpetaPorId(int IdCarpeta)
        {
            List<Carpeta> listaCarpetas = ctx.Carpeta.ToList();
            Carpeta carpeta = new Carpeta();
            carpeta = listaCarpetas.Where(x => x.IdCarpeta == IdCarpeta).FirstOrDefault();
            if(carpeta == null)
            { 
                throw new Exception("Id de carpeta inexistente");
            }
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

      //  public void CrearCarpetaGeneral (int IdUsuario)
      //  {
      //      Carpeta carpeta = new Carpeta();
      //      carpeta.Nombre = "General";
      //      carpeta.Descripcion = "Carpeta por defecto";
      //      carpeta.FechaCreacion = DateTime.Now;
      //      carpeta.IdUsuario = IdUsuario;
      //      ctx.Carpeta.Add(carpeta);
      //      ctx.SaveChanges();
      //  }

        public string autentificarCarpeta (int idCarpeta)
        {
            Carpeta carpeta = BuscarCarpetaPorId(idCarpeta);
            if (new UsuarioM { }.GetUser().IdUsuario != carpeta.IdUsuario)
            {
                return "Listar";
            }

            else return null;
        }


    }
}