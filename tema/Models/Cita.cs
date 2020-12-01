using System;
using System.ComponentModel.DataAnnotations;

namespace tema.Models
{
    public class Cita
    {
        public int IDCita { get; set; }
        [Required(ErrorMessage = "No se ingresó el código del técnico. Puede obtenerlo en el módulo de técnicos")]
        [Display(Name = "Código del técnico")]
        public int? IDTecnico { get; set; }
        //[RegularExpression(@"[1-7]-[0-9]{4}-[0-9]{4}",
        //    ErrorMessage = "Formato requerido (0-0000-0000)")]
        //[Required(ErrorMessage = "No se ingresó la cédula del cliente")]
        [Display(Name = "Cédula")]
        //[StringLength(30, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string cedulaCliente { get; set; }
        [Required(ErrorMessage = "No se ingresó la fecha")]
        [Display(Name = "Fecha")]
        public DateTime? fecha { get; set; }
        [Required(ErrorMessage = "No se ingresó la hora")]
        [Display(Name = "Hora")]
        public string hora { get; set; }
        [Required(ErrorMessage = "No se ingresó el asunto")]
        [Display(Name = "Asunto")]
        [StringLength(100, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string asunto { get; set; }
        [Required(ErrorMessage = "No se ingresó una descripción")]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
        [Display(Name = "¿Cita confirmada?")]
        public bool citaConfirmada { get; set; }
        public int IDCliente { get; set; }
    }
}

