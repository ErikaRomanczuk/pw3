using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManager.Repository;

namespace TaskManager.Models
{
    public class TareaViewModel
    {
        #region Propiedades
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression(@"^([^<>]){1,50}$", ErrorMessage = "El campo {0} es obligatorio y tiene de maximo 50 caracters.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [RegularExpression(@"^([^<>]){1,200}$", ErrorMessage = "El campo {0} es obligatorio y tiene de maximo 200 caracters.")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha Finalizacion")]
        public DateTime? FechaFin { get; set; }

        [Display(Name = "Fecha Creacion")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Prioridad")]
        public short Prioridad { get; set; }

        public string PrioridadDescripcion
        {
            get
            {
                if (Prioridad == 1)
                {
                    return "Baja";
                }
                else if (Prioridad == 2)
                {
                    return "Media";
                }
                else if (Prioridad == 3)
                {
                    return "Alta";
                }
                else if (Prioridad == 4)
                {
                    return "Urgente";
                }
                else
                {
                    return "Sin prioridad";
                }

            }
        }
        
        [Display(Name = "Carpeta")]
        public string Carpeta { get; set; }

        
        [Display(Name = "Carpeta")]
        public int IdCarpeta { get; set; }


        [Display(Name = "Completado")]
        public short Completada { get; set; }

        public string CompletadaDescripcion
        {
            get
            {
                if (Completada == 1)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        [Display(Name = "Horas Estimadas")]
        //[Range(0, 9999999999999999.99, ErrorMessage = "El campo {0} no puede tener más de 2 decimales.")]
        public decimal? EstimadoHoras { get; set; }

        public int IdTarea { get; set; }

        public UsuarioM UsuarioM { get; set; }

        public CarpetaM CarpetaM { get; set; }

        public IList<ArchivoTareaViewModel> ArchivoTarea { get; set; }
        #endregion


        #region Metodos Privados
        /// <summary>
        /// Mapea una TAREA obvteniada de la BBDD a una TareaViewModel para la vista
        /// </summary>
        /// <param name="tarea">Tarea obtenida de la BBDD</param>
        /// <returns>TareaViewModel</returns>
        private void Map(Tarea tarea)
        {
            
            IdTarea = tarea.IdTarea;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            FechaCreacion = tarea.FechaCreacion;

            FechaFin = tarea.FechaFin;
            EstimadoHoras = tarea.EstimadoHoras;
            Prioridad = tarea.Prioridad;

            if (tarea.Usuario != null) // Siempre null por la DB.
            {
                UsuarioRepository usuarioRepository = new UsuarioRepository();
                int idUsuario = (int)tarea.IdUsuario;
                Usuario usuario = usuarioRepository.BuscarUsuarioPorId(idUsuario);
                UsuarioM = new UsuarioM { }.ModelarUsuario(usuario);
            }

            if (tarea.IdCarpeta >= 0)   //  if (tarea.IdCarpeta != null)
            {
                CarpetasRepository carpetasRepository = new CarpetasRepository();
                CarpetaM carpetaModelo = new CarpetaM();
                Carpeta c = carpetasRepository.BuscarCarpetaPorId(tarea.IdCarpeta);
                CarpetaM carpetaM = carpetaModelo.ModelarCarpeta(c);
                CarpetaM = carpetaM;
            }

            ArchivoTarea = tarea.ArchivoTarea.OrderByDescending(x => x.FechaCreacion).Select(x => new ArchivoTareaViewModel(x)).ToList();
        }
        #endregion

        #region Metodos Estaticos
        /// <summary>
        /// Devuelve una instancia de TareaViewModel
        /// </summary>
        /// <param name="tarea">tarea obtenida de la BBDD</param>
        /// <returns>TareaViewModel</returns>
        public static TareaViewModel FromTarea(Tarea tarea)
        {
            TareaViewModel tareaM = new TareaViewModel();
            tareaM.Map(tarea);
            return tareaM;
        }

        public static Tarea ToTarea(TareaViewModel tareaM)
        {
            Tarea tarea = new Tarea();
            tarea.IdTarea = tareaM.IdTarea;
            tarea.IdCarpeta = tareaM.IdCarpeta;
            tarea.Nombre = tareaM.Nombre;
            tarea.Descripcion = tareaM.Descripcion;
            tarea.FechaFin = tareaM.FechaFin;
            tarea.Prioridad = tareaM.Prioridad;
            tarea.Completada = tareaM.Completada;
            tarea.EstimadoHoras = tareaM.EstimadoHoras;
            tarea.Nombre = tareaM.Nombre;

            int idUser = new UsuarioM { }.GetUser().IdUsuario;
            if (idUser > 0)
            {
                tarea.IdUsuario = idUser;
            }

            return tarea;
        }
        #endregion
    }
}