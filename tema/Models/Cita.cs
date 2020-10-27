using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace tema.Models
{
    public class Cita
    {
        public int IDCita { get; set; }
        [Required(ErrorMessage = "No se ingresó el código del técnico. Puede obtenerlo en el módulo de técnicos")]
        [Display(Name = "Código del técnico")]
        public int? IDTecnico { get; set; }
        [RegularExpression(@"[1-7]-[0-9]{4}-[0-9]{4}",
            ErrorMessage = "Formato requerido (0-0000-0000)")]
        [Required(ErrorMessage = "No se ingresó la cédula del cliente")]
        [Display(Name = "Cédula")]
        public string cedulaCliente { get; set; }
        [Required(ErrorMessage = "No se ingresó la fecha")]
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }
        [Required(ErrorMessage = "No se ingresó la hora")]
        [Display(Name = "Hora")]
        public string hora { get; set; }
        [Required(ErrorMessage = "No se ingresó el asunto")]
        [Display(Name = "Asunto")]
        public string asunto { get; set; }
        [Required(ErrorMessage = "No se ingresó una descripción")]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
        [Display(Name = "¿Cita confirmada?")]
        public bool citaConfirmada { get; set; }
        public int IDCliente { get; set; }
    }
}

