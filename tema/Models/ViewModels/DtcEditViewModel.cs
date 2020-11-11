using System.ComponentModel.DataAnnotations;
namespace tema.Models.ViewModels
{
    public class DtcEditViewModel
    {
        public int IDCliente { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Debe ingresar una dirección")]
        public string direccion { get; set; }
        [Display(Name = "Número telefónico")]
        //[RegularExpression(@"^[0-9]$", ErrorMessage = "Solo puede ingresar números enteros")]
        [Required(ErrorMessage = "Debe ingresar el teléfono")]
        public string telefono { get; set; }
        [Display(Name = "Correo")]
        [EmailAddress(ErrorMessage = "No se ingresó un correo válido")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe ingresar el correo")]
        public string correo { get; set; }
        public int codDet { get; set; }
    }
}
