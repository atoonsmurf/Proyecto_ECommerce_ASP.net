using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoMundoTronic.Models
{
    public class Cliente
    {
        public string cod_cli { get; set; }
        public string dni { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string direccion { get; set; }


        public int cod_dist { get; set; }

        public string celular { get; set; }

        public string fijo { get; set; }

        public string correo { get; set; }


        public string clave { get; set; }

       
    }
}