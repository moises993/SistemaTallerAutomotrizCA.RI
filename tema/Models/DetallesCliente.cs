using System.ComponentModel.DataAnnotations;

namespace tema.Models
{
    public class DetallesCliente
    {
        public int IDCliente { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Debe ingresar una dirección")]
        public string direccion { get; set; }
        [Display(Name = "Número telefónico")]
        [StringLength(25, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string telefono { get; set; }
        [Display(Name = "Correo")]
        [EmailAddress(ErrorMessage = "No se ingresó un correo válido")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe ingresar el correo")]
        [StringLength(35, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string correo { get; set; }
        public int codDet { get; set; }
        [StringLength(25, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string telefonoExt { get; set; }
    }
}
