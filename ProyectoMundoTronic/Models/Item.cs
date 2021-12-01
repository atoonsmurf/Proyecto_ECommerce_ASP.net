using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoMundoTronic.Models
{
    public class Item
    {
        public String codigo { get; set; }
        public string nombre { get; set; }

        public string descripcion { get; set; }
        public int categoria { get; set; }

        public decimal precio { get; set; }

        public int cantidad { get; set; }

        public decimal monto { get { return precio * cantidad; } }
    }
}