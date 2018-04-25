using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class ComentarioTarea
    {
        public int IdComentarioTarea { get; set; }

        [Display(Name = "Comentario")]
        public string Texto { get; set; }

        [Display(Name = "Feccha de creacion")]
        public DateTime FechaCreacion { get; set; }

        //  public int IdTarea { get; set; }
    }
}