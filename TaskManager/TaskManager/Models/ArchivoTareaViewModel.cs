using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class ArchivoTareaViewModel
    {
        public int IdArchivo { get; set; }

        [Display(Name = "Ruta")]
        public string RutaArchivo { get; set; }
        [Display(Name = "Fecha")]
        public DateTime FechaCreacion { get; set; }

        public int IdTarea { get; set; }

        public TareaViewModel Tarea { get; set; }

        public ArchivoTareaViewModel Map(ArchivoTarea at)
        {
            ArchivoTareaViewModel a = new ArchivoTareaViewModel();
            return a;
        }
    }
}