using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ProyectoMundoTronic.Models
{
    public class Producto
    {
        [Required]
        public string codpro { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string descripcion { get; set; }
        [Required]
        public int categoria { get; set; }
        [Required]
        public int stock { get; set; }
        [Required]
        public decimal precio { get; set; }
    }
}