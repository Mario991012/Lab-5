using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Unidad_5_Lab.Models
{
    public class Informacion
    {
        public List<Estampilla> Faltantes { get; set; }
        public List<Estampilla> Disponibles { get; set; }
        public List<Estampilla> Coleccionadas { get; set; }
        public List<Estampilla> Todo { get; set; }

        public Informacion()
        {
            Faltantes = new List<Estampilla>();
            Todo = new List<Estampilla>();
            Disponibles = new List<Estampilla>();
            Coleccionadas = new List<Estampilla>();
        }
    }
}