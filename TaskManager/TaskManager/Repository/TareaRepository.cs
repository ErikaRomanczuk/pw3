using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class TareasRepository
    {

        private static List<Tarea> Tareas = new List<Tarea>();

        public List<Tarea> listarTodos()
        {
            Tarea tarea1 = new Tarea();
            tarea1.Nombre = "Tarea1";
            tarea1.Descripcion = "Descripcion tarea 1";
            tarea1.EstimadoHoras = 10;
            tarea1.FechaFin = DateTime.Now;
            tarea1.Prioridad = 1;

            Tarea tarea2 = new Tarea();
            tarea2.Nombre = "Tarea2";
            tarea2.Descripcion = "Descripcion tarea 2";
            tarea2.EstimadoHoras = 15;
            tarea2.FechaFin = DateTime.Now;
            tarea2.Prioridad = 2;

            Tarea tarea3 = new Tarea();
            tarea3.Nombre = "Tarea3";
            tarea3.Descripcion = "Descripcion tarea 3";
            tarea3.EstimadoHoras = 20;
            tarea3.FechaFin = DateTime.Now;
            tarea3.Prioridad = 3;

            Tarea tarea4 = new Tarea();
            tarea4.Nombre = "Tarea4";
            tarea4.Descripcion = "Descripcion tarea 4";
            tarea4.EstimadoHoras = 25;
            tarea4.FechaFin = DateTime.Now;
            tarea4.Prioridad = 4;

            Tareas.Add(tarea1);
            Tareas.Add(tarea2);
            Tareas.Add(tarea3);
            Tareas.Add(tarea4);

            return Tareas;
        }
    }
}