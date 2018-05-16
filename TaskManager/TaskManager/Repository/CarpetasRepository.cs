using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class CarpetasRepository
    {
        public List<Carpeta> carpetas = new List<Carpeta>();

        public List<Carpeta> listarCarpetas()
        {
            Carpeta c1 = new Carpeta();
            c1.IdCarpeta = 1;
            c1.Nombre = "Carpeta 1";
            c1.Descripcion = "Descripción carpeta 1";

            Carpeta c2 = new Carpeta();
            c2.IdCarpeta = 2;
            c2.Nombre = "Carpeta 2";
            c2.Descripcion = "Descripción carpeta 2";

            Carpeta c3 = new Carpeta();
            c3.IdCarpeta = 3;
            c3.Nombre = "Carpeta 3";
            c3.Descripcion = "Descripción carpeta 3";

            Carpeta c4 = new Carpeta();
            c4.IdCarpeta = 4;
            c4.Nombre = "Carpeta 4";
            c4.Descripcion = "Descripción carpeta 4";

            carpetas.Add(c1);
            carpetas.Add(c2);
            carpetas.Add(c3);
            carpetas.Add(c4);

            return (carpetas);
        }

        public void CrearCarpeta(Carpeta carpeta)
        {
            carpetas = listarCarpetas();
            Carpeta nuevaCarpeta = new Carpeta();
            nuevaCarpeta.Nombre = carpeta.Nombre;
            nuevaCarpeta.Descripcion = carpeta.Descripcion;

            carpetas.Add(nuevaCarpeta);
        }

        public Carpeta BuscarCarpetaPorId(int Id)
        {
            carpetas = listarCarpetas();
            Carpeta carpeta = new Carpeta();
            carpeta = carpetas.Where(x => x.IdCarpeta == Id).FirstOrDefault();
            if (carpeta == null)
            {
                throw new ArgumentException("Carpeta con id: " + carpeta.IdCarpeta + " es inexistente");
            }
            return carpeta;
            //return carpeta;
        }

        public void ModificarCarpeta(Carpeta carpeta)
        {
            Carpeta CarpetaActual = BuscarCarpetaPorId(carpeta.IdCarpeta);
            if (CarpetaActual == null)
            {
                throw new ArgumentException("Carpeta con id: " + carpeta.IdCarpeta + " es inexistente");
            }

            CarpetaActual.Nombre = carpeta.Nombre;
            CarpetaActual.Descripcion = carpeta.Descripcion;

            carpetas.Add(CarpetaActual);
        }
    }
}