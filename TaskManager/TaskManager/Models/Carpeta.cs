using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class Carpeta
    {
        public int IdCarpeta { get; set;}
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        // public Usuario IdUsuario { get; set; }
    }
}