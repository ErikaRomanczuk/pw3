using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class UsuarioM
    {
        
        public int IdUsuario { get; set; }

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
        [Display(Name = "Contrasena *")]
        public string Contrasena { get; set; }

        [Display(Name = "Activo")]
        public int Activo { get; set; }

        [Display(Name = "Fecha de registracion")]
        public DateTime FechaRegistracion { get; set; }

        [Display(Name = "Fecha de activacion")]
        public DateTime FechaActivacion { get; set; }

        [Display(Name = "Codigo de activacion")]
        public string CodigoActivacion { get; set; }

        public List<CarpetaM> Carpetas { get; set; }

        public List<Tarea> Tarea { get; set; }

        public string ErrorRegistro;
    }
}