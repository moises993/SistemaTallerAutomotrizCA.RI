using System.ComponentModel.DataAnnotations;

namespace tema.Models.ViewModels
{
    public class DttEditViewModel
    {
        public int IDTecnico { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Debe ingresar una dirección")]
        public string direccion { get; set; }
        [Display(Name = "Número telefónico")]
        //[RegularExpression(@"^[0-9]$", ErrorMessage = "Solo puede ingresar números enteros")]
        [Required(ErrorMessage = "Debe ingresar el teléfono")]
        [StringLength(25, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string telefono { get; set; }
        [Display(Name = "Correo")]
        [EmailAddress(ErrorMessage = "No se ingresó un correo válido")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe ingresar el correo")]
        [StringLength(35, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string correo { get; set; }
        public int codDet { get; set; }
    }
}
