using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Web;

namespace Unidad_5_Lab.Models
{
    public class Estampilla
    {
        [DisplayName("Numero de estapilla")]
        public int numero { get; set; }
        [DisplayName("Cantidad de misma estampilla")]
        public int cantidad { get; set; }
        [DisplayName("Obtenida")]
        public bool obtenida { get; set; }

        public Estampilla()
        {
            numero = 0;
            cantidad = 0;
            obtenida = false;
        }
    }
}
