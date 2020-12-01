using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tema.Models
{
    public class Cliente
    {
        public int IDCliente { get; set; }
        [Required(ErrorMessage = "No se ingresó el nombre del cliente")]
        [Display(Name = "Nombre")]
        [StringLength(150, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string nombre { get; set; }
        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "No se ingresó el primer apellido del cliente")]
        [StringLength(50, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string pmrApellido { get; set; }
        [Display(Name = "Segundo Apellido")]
        [Required(ErrorMessage = "No se ingresó el segundo apellido del cliente")]
        [StringLength(50, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string sgndApellido { get; set; }
        
        [Display(Name = "Cédula")]
        [StringLength(30, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string cedula { get; set; }
        [Display(Name = "Cliente Frecuente")]
        [Required(ErrorMessage = "No se indicó si el cliente es frecuente o nuevo")]
        public bool cltFrecuente { get; set; }
        [Display(Name = "Fecha de Ingreso")]
        public DateTime fechaIngreso { get; set; }
        //para la vista de crear

        [StringLength(30, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string cedExt { get; set; }
    }
} //[1-7]-[0-9]{4}-[0-9]{4}
