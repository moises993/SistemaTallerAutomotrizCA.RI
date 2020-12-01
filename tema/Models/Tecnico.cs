using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tema.Models
{
    public class Tecnico
    {
        public int IDTecnico { get; set; }
        [Required(ErrorMessage = "No se ingresó el nombre del técnico")]
        [Display(Name = "Nombre")]
        [StringLength(150, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string nombre { get; set; }
        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "No se ingresó el primer apellido del técnico")]
        [StringLength(50, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string pmrApellido { get; set; }
        [Display(Name = "Segundo Apellido")]
        [Required(ErrorMessage = "No se ingresó el segundo apellido del técnico")]
        [StringLength(50, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string sgndApellido { get; set; }
        //[RegularExpression(@"[1-7]-[0-9]{4}-[0-9]{4}",
        // ErrorMessage = "Formato requerido (0-0000-0000)")]
        [Display(Name = "Cédula")]
        //[Required(ErrorMessage = "No se ingresó la cédula del cliente")]
        [StringLength(30, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string cedula { get; set; }
        [Display(Name = "Fecha de ingreso")]
        public DateTime fechaIngreso { get; set; }
        [StringLength(30, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string cedExt { get; set; }
    }
}





