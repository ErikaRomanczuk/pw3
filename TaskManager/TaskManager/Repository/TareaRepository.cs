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
        CarpetaM carpetaModelo = new CarpetaM();
        TareaViewModel tareaModelo = new TareaViewModel();

        public List<Tarea> ListarTodos()
        {
            int idUser = new UsuarioM { }.GetUser().IdUsuario;
            if (idUser < 0)
            {
                return null;
            }
            
            List<Tarea> tareas = ctx.Tarea.Where(x => x.IdUsuario == idUser)
                                          .OrderByDescending(x => x.FechaCreacion)
                                          .ToList();
            return tareas;
        }

        public List<Tarea> ListarConFiltroCompletado(String completado)
        {
        
            int filtro = int.Parse(completado);
            List<Tarea> tareas  = ctx.Tarea.Where(x => x.Completada == filtro && x.IdUsuario ==new UsuarioM { }.GetUser().IdUsuario)
                                           .OrderByDescending(x => x.FechaCreacion)
                                           .ToList();

            return tareas;
        }

        /// <summary>
        /// Lista de tareas ordenadas por prioridad y fecha de vencimiento
        /// </summary>
        /// <param name="usuarioID">Se filtra por ID para traer las tareas de ese usuario</param>
        /// <returns>Lista de TAREAS filtradas</returns>
        public List<Tarea> TareasPriotarias(int usuarioID)
        {
            List<Tarea> tareas = ctx.Tarea.Where(x => x.Completada == 0 && x.IdUsuario == usuarioID)
                                          .OrderByDescending(x => x.FechaFin)
                                          .OrderByDescending(x => x.Prioridad)
                                          .ToList();
            return tareas;
        }


        public Tarea buscarPorIdTarea(int id)
        {
            Tarea tarea = ctx.Tarea.Include("ArchivoTarea").FirstOrDefault(x => x.IdTarea == id);
            if (tarea == null)
            {
                throw new Exception("Id de tarea inexistente");
            }
            return tarea;
        }

        //    public void Crear(TareaViewModel tareaM, String idCarpeta)
        //    {
        //        Tarea tarea = new Tarea();
        //        tarea.Nombre = tareaM.Nombre;
        //        tarea.Descripcion = tareaM.Descripcion;
        //        tarea.FechaFin = tareaM.FechaFin;
        //        tarea.FechaCreacion = DateTime.Now;
        //        tarea.Prioridad = tareaM.Prioridad;
        //        tarea.Completada = tareaM.Completada;
        //        tarea.EstimadoHoras = tareaM.EstimadoHoras;
        //        int idCarpeta2 = int.Parse(idCarpeta);
        //        tarea.IdCarpeta = idCarpeta2;
        //        tarea.Nombre = tareaM.Nombre;
        //        tarea.IdUsuario = loginRepository.GetUser().IdUsuario;
        //        ctx.Tarea.Add(tarea);
        //        ctx.SaveChanges();
        //    }

        public void Crear(Tarea tarea)
        {
            tarea.FechaCreacion = DateTime.Now;
            ctx.Tarea.Add(tarea);
            ctx.SaveChanges();
        }


        public void Borrar(int id)
        {
            Tarea tarea = buscarPorIdTarea(id);
            ctx.Tarea.Remove(tarea);
            ctx.SaveChanges();
        }


        public void CompletarPorIdTarea(int id)
        {
            Tarea tarea = buscarPorIdTarea(id);
            tarea.Completada = 1;
            ctx.SaveChanges();  
        }

        public Tarea Modificar(Tarea tareaM)
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
                tarea.IdCarpeta = tareaM.IdCarpeta;

                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Bool" + e);
            }
            return tarea;
        }

        public List<Tarea> ListarTareasDeCarpeta(int idCarpeta)
        {
            // List<TareaViewModel> listaTareaM = ListarTodos();
            // List<TareaViewModel> listaDeTareasDeCarpeta = listaTareaM.Where(x => x.CarpetaM.IdCarpeta == idCarpeta).ToList();
            // return listaDeTareasDeCarpeta;

            List<Tarea> list = ctx.Tarea.Where(x => x.IdCarpeta == idCarpeta).ToList();
            return list;
        }

        internal void AgregarArchivo(int idTarea, string filePath)
        {
            Tarea tarea = buscarPorIdTarea(idTarea);
            tarea.ArchivoTarea.Add(new ArchivoTarea()
            {
                RutaArchivo = filePath,
                FechaCreacion = DateTime.Now
            });
            ctx.SaveChanges();
        }
    }
}