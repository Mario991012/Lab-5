using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Unidad_5_Lab.Models
{
    public class Diccionario2Info
    {
        [DisplayName("Equipo")]
        public string Equipo { get; set; }
        [DisplayName("Numero de estampa faltante")]
        public string NumeroEstampa { get; set; }

        public Diccionario2Info()
        {
            Equipo = "";
            NumeroEstampa = "";
        }
    }
}