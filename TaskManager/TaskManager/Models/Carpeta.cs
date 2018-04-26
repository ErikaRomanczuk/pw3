using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class Carpeta
    {
        public int IdCarpeta { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression(@"^([^<>]){1,50}$", ErrorMessage = "El campo {0} es obligatorio y tiene un máximo 50 caracteres.")]
        [Display(Name = "Nombre *")]
        public string Nombre { get; set; }

        [RegularExpression(@"^([^<>]){1,200}$", ErrorMessage = "El campo {0} tiene de máximo 200 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }
        // public Usuario IdUsuario { get; set; }
    }
}