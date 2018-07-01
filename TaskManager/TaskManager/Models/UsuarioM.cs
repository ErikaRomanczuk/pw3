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
        [StringLength(50, ErrorMessage = "El campo {0} tiene de maximo 50 caracteres.")]
        [Display(Name = "Nombre *")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo {0} tiene de maximo 50 caracteres.")]
        [Display(Name = "Apellido *")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(200, ErrorMessage = "El campo {0} tiene de maximo 200 caracteres.")]
        [EmailAddress(ErrorMessage ="Ingresar un Email Valido")]
        [Display(Name = "Email *")]
        public string Email { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{0,20}$", ErrorMessage = "El campo {0} debe contener como maximo 20 caracteres, almenos una Mayuscula, una Minuscula y un Numero")]
        [Display(Name = "Contraseña *")]
        public string Contrasena { get; set; }

       [Required(ErrorMessage = "El campo {0} es obligatorio.")]
       [Compare(otherProperty: "Contrasena", ErrorMessage ="Las Contraseñas no coinciden")]
        public string Contrasena2 { get; set; }

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