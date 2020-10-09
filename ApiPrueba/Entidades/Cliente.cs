using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Entidades
{
    public class Cliente
    {
        public int IDCliente { get; set; }
        [Required(ErrorMessage = "No se ingresó el nombre del cliente")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "No se ingresó el primer apellido del cliente")]
        public string pmrApellido { get; set; }
        [Required(ErrorMessage = "No se ingresó el segundo apellido del cliente")]
        public string sgndApellido { get; set; }
        [Required(ErrorMessage = "No se ingresó la cédula del cliente")]
        public string cedula { get; set; }
        [Required(ErrorMessage = "No se indicó si el cliente es frecuente o nuevo")]
        public bool cltFrecuente { get; set; }
        public DateTime fechaIngreso { get; set; }
    }
}
