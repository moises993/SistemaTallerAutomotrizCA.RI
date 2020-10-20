using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tema.Models
{
    public class DetallesCliente
    {
        public int IDCliente { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Debe ingresar una dirección")]
        public string direccion { get; set; }
        [Display(Name = "Número telefónico")]
        [Required(ErrorMessage = "Debe ingresar un número")]
        public string telefono { get; set; }
        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Debe ingresar el correo")]
        public string correo { get; set; }
        public int codDet { get; set; }
    }
}
