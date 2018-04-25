using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class Usuario
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression(@"^([^<>]){1,50}$", ErrorMessage = "El campo {0} es obligatorio y tiene de maximo 50 caracters.")]
        [Display(Name = "Nombre *")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression(@"^([^<>]){1,50}$", ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Apellido *")]
        public string Apellido { get; set; }

        //TODO: Buscar experesion regular para email
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression(@"^([^<>]){1,50}$", ErrorMessage = "Debe ser un email valido.")]
        [Display(Name = "Email *")]
        public string Email { get; set; }

        //TODO: Buscar experesion regular para contraseña
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression(@"^([^<>]){1,50}$", ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Contraseña *")]
        public string Contraseña { get; set; }

        [Display(Name = "Activo")]
        public int Activo { get; set; }

        [Display(Name = "Fecha de registracion")]
        DateTime FechaRegistracion { get; set; }

        [Display(Name = "Fecha de activacion")]
        DateTime FechaActivacion { get; set; }

        [Display(Name = "Codigo de activacion")]
        string CodigoActivacion { get; set; }

        public List<Carpeta> Carpetas { get; set; }

        public List<Tarea> Tarea { get; set; }
    }
}