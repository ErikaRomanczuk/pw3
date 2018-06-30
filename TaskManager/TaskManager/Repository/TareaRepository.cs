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
        LoginRepository loginRepository = new LoginRepository();
        UsuarioRepository usuarioRepository = new UsuarioRepository();
        CarpetasRepository carpetasRepository = new CarpetasRepository();

        public List<TareaM> listarTodos()
        {
            Context ctx = new Context();
            List<Tarea> tareas = new List<Tarea>();
            tareas = ctx.Tarea.ToList();
            tareas = tareas.Where(x => x.IdUsuario == loginRepository.GetUser().IdUsuario).ToList();
            List<TareaM> tareasM = new List<TareaM>();

            foreach (var tareaEF in tareas)
            {
                TareaM tarea = ModelarTarea(tareaEF);
                tareasM.Add(tarea);
            }

            return tareasM;
        }

        public List<TareaM> listarConFiltroCompletado(String completado)
        {
            List<Tarea> tareas = new List<Tarea>();
            int filtro = int.Parse(completado);
            tareas = ctx.Tarea.Where(x => x.Completada == filtro && x.IdUsuario == loginRepository.GetUser().IdUsuario).ToList();
            List<TareaM> tareasM = new List<TareaM>();

            foreach (var tareaEF in tareas)
            {
                TareaM tarea = ModelarTarea(tareaEF);
                tareasM.Add(tarea);
            }

            return tareasM;
        }

        public Tarea buscarPorIdTarea(int id)
        {
            List<Tarea> tareas = ctx.Tarea.ToList();
            Tarea tarea = tareas.Where(x => x.IdTarea == id).FirstOrDefault();
            if (tarea == null)
            {
                throw new Exception("Id de carpeta inexistente");
            }
            return tarea;
        }

        public void Crear(TareaM tareaM, String idCarpeta)
        {
            Tarea tarea = new Tarea();
            tarea.Nombre = tareaM.Nombre;
            tarea.Descripcion = tareaM.Descripcion;
            tarea.FechaFin = tareaM.FechaFin;
            tarea.FechaCreacion = DateTime.Now;
            tarea.Prioridad = tareaM.Prioridad;
            tarea.Completada = tareaM.Completada;
            tarea.EstimadoHoras = tareaM.EstimadoHoras;
            int idCarpeta2 = int.Parse(idCarpeta);
            tarea.IdCarpeta = idCarpeta2;
            tarea.Nombre = tareaM.Nombre;
            tarea.IdUsuario = loginRepository.GetUser().IdUsuario;
            ctx.Tarea.Add(tarea);
            ctx.SaveChanges();
        }


        public void Borrar(int id)
        {
            Tarea tarea = buscarPorIdTarea(id);
            ctx.Tarea.Remove(tarea);
            ctx.SaveChanges();
        }

        public Tarea Modificar(TareaM tareaM, string idCarpeta)
        {
            Tarea tarea = buscarPorIdTarea(tareaM.IdTarea);
            if (tarea == null)
            {
                throw new ArgumentException("Tarea con id: " + tarea.IdTarea + " es inexistente");
            }
            try
            {
                tarea.Nombre = tareaM.Nombre;
                tarea.Descripcion = tareaM.Descripcion;
                tarea.FechaFin = tareaM.FechaFin;
                tarea.Prioridad = tareaM.Prioridad;
                tarea.Completada = tareaM.Completada;
                tarea.EstimadoHoras = tareaM.EstimadoHoras;
                tarea.Nombre = tareaM.Nombre;
                tarea.IdCarpeta = int.Parse(idCarpeta);

                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Bool" + e);
            }
            return tarea;
        }

        public TareaM ModelarTarea(Tarea tarea)
        {
            TareaM tareaM = new TareaM();

            tareaM.IdTarea = tarea.IdTarea;
            tareaM.Nombre = tarea.Nombre;
            tareaM.Descripcion = tarea.Descripcion;
            tareaM.FechaFin = tarea.FechaFin;
            tareaM.FechaCreacion = tarea.FechaCreacion;
            tareaM.Prioridad = tarea.Prioridad;
            tareaM.Completada = tarea.Completada;
            tareaM.EstimadoHoras = tarea.EstimadoHoras;
            tareaM.Nombre = tarea.Nombre;

            if (tarea.IdUsuario != null)
            {
                int idUsuario = tarea.IdUsuario;
                Usuario usuario = usuarioRepository.BuscarUsuarioPorId(idUsuario);
                tareaM.UsuarioM = usuarioRepository.ModelarUsuario(usuario);
            }

            //if (tarea.IdCarpeta != null)
            //{
            //    Carpeta c = carpetasRepository.BuscarCarpetaPorId(tarea.IdCarpeta);
            //    CarpetaM carpetaM = carpetasRepository.ModelarCarpeta(c);
            //    tareaM.CarpetaM = carpetaM;
            //}

            return tareaM;
        }
    }
}