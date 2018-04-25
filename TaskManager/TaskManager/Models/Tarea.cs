using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class Tarea
    {

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression(@"^([^<>]){1,50}$", ErrorMessage = "El campo {0} es obligatorio y tiene de maximo 50 caracters.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression(@"^([^<>]){1,200}$", ErrorMessage = "El campo {0} es obligatorio y tiene de maximo 50 caracters.")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Fecha Finalizacion")]
        public DateTime FechaFin { get; set; }

        [Display(Name = "Fecha Creacion")]
        public DateTime FechaCreacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Prioridad")]
        public int Prioridad { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Carpeta")]
        public string Carpeta { get; set; }

        [Display(Name = "Estado")]
        public int Completada { get; set; }

        [Display(Name = "Horas Estimadas")]
        public decimal EstimadoHoras { get; set; }

        public int IdTarea { get; set; }
        //    IdCarpeta
        //    IdUsuario

    }
}