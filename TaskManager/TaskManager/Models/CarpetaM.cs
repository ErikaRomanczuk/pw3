using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManager.Repository;

namespace TaskManager.Models
{
    public class CarpetaM
    {
        public int IdCarpeta { get; set; }

        public UsuarioM Usuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression(@"^([^<>]){1,50}$", ErrorMessage = "El campo {0} es obligatorio y tiene un máximo 50 caracteres.")]
        [Display(Name = "Nombre *")]
        public string Nombre { get; set; }

        [RegularExpression(@"^([^<>]){0,200}$", ErrorMessage = "El campo {0} tiene de máximo 200 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }
        // public Usuario IdUsuario { get; set; }

        UsuarioRepository usuarioRepository = new UsuarioRepository();

        public CarpetaM ModelarCarpeta(Carpeta carpeta)
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
                Usuario usuario = usuarioRepository.BuscarUsuarioPorId(idUsuario);
                carpetaM.Usuario = new UsuarioM{ }.ModelarUsuario(usuario);

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
  //          carpeta.IdUsuario = carpetaM.Usuario.IdUsuario;

            return carpeta;
        }

    }
}