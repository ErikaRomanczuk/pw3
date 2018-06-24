using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class TareaRepository
    {
        Context ctx = new Context();
        public List<TareaM> listarTodos()
        {
            List<Tarea> tareas = new List<Tarea>();
            tareas = ctx.Tarea.ToList();
            List<TareaM> tareasM = new List<TareaM>();

            foreach(var tareaEF in tareas)
            {
                TareaM tarea = new TareaM();
                tarea.IdTarea = tareaEF.IdTarea;
                tarea.Nombre = tareaEF.Nombre;
                tarea.Descripcion = tareaEF.Descripcion;
                tarea.FechaFin= tareaEF.FechaFin;
                tarea.FechaCreacion = tareaEF.FechaCreacion;
                tarea.Prioridad = tareaEF.Prioridad;
                // carpeta??
                tarea.Completada = tareaEF.Completada;
                tarea.EstimadoHoras = tareaEF.EstimadoHoras;
                tarea.Usuario = tareaEF.Usuario;
                tareasM.Add(tarea);
            }

            return tareasM;
        }

        public Tarea buscarPorIdTarea(int id)
        {
            List<Tarea> tareas = ctx.Tarea.ToList();
            Tarea tarea = tareas.Where(x => x.IdTarea == id).FirstOrDefault();
            if( tarea == null)
            {
                throw new Exception("Id de carpeta inexistente");
            }
            return tarea;
        }

        public void crear(TareaM tareaM)
        {
            Tarea tarea = new Tarea();
            tarea.Nombre = tareaM.Nombre;
            tarea.Descripcion = tareaM.Descripcion;
            tarea.FechaFin = tareaM.FechaFin;
            tarea.FechaCreacion = tareaM.FechaCreacion;
            tarea.Prioridad = tareaM.Prioridad;
            // carpeta??
            tarea.Completada = tareaM.Completada;
            tarea.EstimadoHoras = tareaM.EstimadoHoras;
            tarea.Nombre = tareaM.Nombre;
            ctx.Tarea.Add(tarea);
            ctx.SaveChanges();
        }


        public void Borrar(int id)
        {
            Tarea tarea = buscarPorIdTarea(id);
            ctx.Tarea.Remove(tarea);
            ctx.SaveChanges();
        }

        public Tarea Modificar(TareaM tareaM)
        {
            Tarea tarea = buscarPorIdTarea(tareaM.IdTarea);
            if ( tarea == null )
            {
                throw new ArgumentException("Carpeta con id: " + tarea.IdTarea + " es inexistente");
            }
            tarea.Nombre = tareaM.Nombre;
            tarea.Descripcion = tareaM.Descripcion;
            tarea.FechaFin = tareaM.FechaFin;
            tarea.FechaCreacion = tareaM.FechaCreacion;
            tarea.Prioridad = tareaM.Prioridad;
            // carpeta??
            tarea.Completada = tareaM.Completada;
            tarea.EstimadoHoras = tareaM.EstimadoHoras;
            tarea.Nombre = tareaM.Nombre;

            ctx.Tarea.Add(tarea);
            ctx.SaveChanges();
            return tarea;
        }
    }
}