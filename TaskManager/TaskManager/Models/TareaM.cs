using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManager.Repository;

namespace TaskManager.Models
{
    public class TareaM
    {

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression(@"^([^<>]){1,50}$", ErrorMessage = "El campo {0} es obligatorio y tiene de maximo 50 caracters.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [RegularExpression(@"^([^<>]){1,200}$", ErrorMessage = "El campo {0} es obligatorio y tiene de maximo 50 caracters.")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Fecha Finalizacion")]
        public DateTime? FechaFin { get; set; }

        [Display(Name = "Fecha Creacion")]
        public DateTime FechaCreacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Prioridad")]
        public short Prioridad { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Carpeta")]
        public string Carpeta { get; set; }

        [Display(Name = "Completado")]
        public short Completada { get; set; }

        [Display(Name = "Horas Estimadas")]
        public decimal? EstimadoHoras { get; set; }

        public int IdTarea { get; set; }

        public UsuarioM UsuarioM { get; set; }

        public CarpetaM CarpetaM { get; set; }

        public TareaM ModelarTarea(Tarea tarea)
        {
            TareaM tareaM = new TareaM();
            tareaM.IdTarea = tarea.IdTarea;
            tareaM.Nombre = tarea.Nombre;
            tareaM.Descripcion = tarea.Descripcion;
            tareaM.FechaCreacion = tarea.FechaCreacion;

            tareaM.FechaFin = tarea.FechaFin;
            tareaM.EstimadoHoras = tarea.EstimadoHoras;
            tareaM.Prioridad = tarea.Prioridad;

            if (tarea.IdUsuario != null) // Siempre null por la DB.
            {
                UsuarioRepository usuarioRepository = new UsuarioRepository();
                int idUsuario = (int)tarea.IdUsuario;
                Usuario usuario = usuarioRepository.BuscarUsuarioPorId(idUsuario);
                tareaM.UsuarioM = usuarioRepository.ModelarUsuario(usuario);
            }
            
            if (tarea.IdCarpeta != null)
            {
                CarpetasRepository carpetasRepository = new CarpetasRepository();
                CarpetaM carpetaModelo = new CarpetaM();
                Carpeta c = carpetasRepository.BuscarCarpetaPorId(tarea.IdCarpeta);
                CarpetaM carpetaM = carpetaModelo.ModelarCarpeta(c);
                tareaM.CarpetaM = carpetaM;
            }

            return tareaM;
        }

    }
}