using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class DetalleTareaRepository
    {
        Context ctx = new Context();
        TareaRepository tareaRepository = new TareaRepository();

        public void Crear (ComentarioTareaM comentarioTareaM, string IdTarea)
        {
            ComentarioTarea comentarioTarea = new ComentarioTarea();
            comentarioTarea.Texto = comentarioTareaM.Texto;
            comentarioTarea.FechaCreacion = DateTime.Now;
            comentarioTarea.IdTarea = int.Parse(IdTarea);
            ctx.ComentarioTarea.Add(comentarioTarea);
            ctx.SaveChanges();
        }

        public ComentarioTarea buscarComentarioTareaPorId (int id)
        {
            ComentarioTarea comentarioTarea = ctx.ComentarioTarea.Where(x => x.IdComentarioTarea == id).FirstOrDefault();
            return comentarioTarea;
        }

        public List<ComentarioTareaM> Listar(int IdTarea)
        {
            List<ComentarioTarea> listaComentarioTarea = ctx.ComentarioTarea.Where(x => x.IdTarea == IdTarea)
                                                                            .OrderBy(x => x.FechaCreacion)
                                                                            .ToList();
            List<ComentarioTareaM> listaComentarioTareaM = new List<ComentarioTareaM>();
            ComentarioTareaM MapearComentarioTareaM = new ComentarioTareaM();
            foreach(var x in listaComentarioTarea)
            {
                ComentarioTareaM ComentarioTareaM = MapearComentarioTareaM.ComentarioTareaMapeo(x);
                listaComentarioTareaM.Add(ComentarioTareaM);
            }
            return listaComentarioTareaM;
        }

        public void Eliminar(int IdComentarioTarea)
        {
            ctx.ComentarioTarea.Remove(buscarComentarioTareaPorId(IdComentarioTarea));
            ctx.SaveChanges();
        }

        public void Modificar(ComentarioTareaM comentarioTareaM)
        {
            ComentarioTarea comentarioTarea = buscarComentarioTareaPorId(comentarioTareaM.IdComentarioTarea);
            comentarioTarea.Texto = comentarioTareaM.Texto;
            ctx.SaveChanges();
        }
    }
}