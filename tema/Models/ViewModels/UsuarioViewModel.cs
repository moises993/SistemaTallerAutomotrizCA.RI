using System.ComponentModel.DataAnnotations;

namespace tema.Models.ViewModels
{
    public class UsuarioViewModel
    {
        [EmailAddress(ErrorMessage = "No se ingresó un correo válido")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "No se ingresó un correo")]
        [StringLength(35, ErrorMessage = "El nombre ingresado excede el límite de caracteres permitido")]
        public string correo { get; set; }
        #nullable enable
        [Required(ErrorMessage = "Debe seleccionar un rol")]
        public string? rol { get; set; }
        #nullable disable
    }
}
