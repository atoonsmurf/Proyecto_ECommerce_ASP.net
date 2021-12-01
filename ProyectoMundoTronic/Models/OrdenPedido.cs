using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoMundoTronic.Models
{
    public class OrdenPedido
    {
        [Required]
        public string cliente { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string fono { get; set; }
    }
}