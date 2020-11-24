using System.ComponentModel.DataAnnotations;

namespace tema.Models.ViewModels
{
    public class UsuarioViewModel
    {
        [EmailAddress(ErrorMessage ="El correo no posee el formato correcto")]
        [Required(ErrorMessage = "No se ingresó un correo")]
        public string correo { get; set; }
        #nullable enable
        [Required(ErrorMessage = "Debe seleccionar un rol")]
        public string? rol { get; set; }
        #nullable disable
    }
}
