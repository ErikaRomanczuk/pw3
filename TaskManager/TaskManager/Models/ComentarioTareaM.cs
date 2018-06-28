using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManager.Repository;

namespace TaskManager.Models
{
    public class ComentarioTareaM
    {
        public int IdComentarioTarea { get; set; }

        [Display(Name = "Comentario")]
        public string Texto { get; set; }

        [Display(Name = "Feccha de creacion")]
        public DateTime FechaCreacion { get; set; }

        public TareaM TareaM { get; set; }

        //  public int IdTarea { get; set; }

        TareaRepository tareaRepository = new TareaRepository();
        public ComentarioTareaM ComentarioTareaMapeo (ComentarioTarea comentarioTarea)
        {
            ComentarioTareaM comentarioTareaM = new ComentarioTareaM();
            comentarioTareaM.IdComentarioTarea = comentarioTarea.IdComentarioTarea;
            comentarioTareaM.Texto = comentarioTarea.Texto;
            comentarioTareaM.FechaCreacion = comentarioTarea.FechaCreacion;
            Tarea tarea = tareaRepository.buscarPorIdTarea(comentarioTarea.IdTarea);
            TareaM tareaM = tareaRepository.ModelarTarea(tarea);
            comentarioTareaM.TareaM = tareaM;
            return comentarioTareaM;

        }
    }
}