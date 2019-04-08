using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Unidad_5_Lab.Models
{
    public class Estampilla
    {
        public int numero { get; set; }
        public int cantidad { get; set; }

        public Estampilla()
        {
            numero = 0;
            cantidad = 0;
        }
    }
}