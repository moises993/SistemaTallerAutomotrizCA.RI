using System.ComponentModel.DataAnnotations;

namespace tema.Models.ViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "No se ingresó un correo")]
        public string correo { get; set; }
        #nullable enable
        [Required(ErrorMessage = "Debe seleccionar un rol")]
        public string? rol { get; set; }
        #nullable disable
    }
}
