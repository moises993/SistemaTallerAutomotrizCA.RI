using System.ComponentModel.DataAnnotations;

namespace tema.Models
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "No se ingresó un correo")]
        public string correoForm { get; set; }
        [Required(ErrorMessage = "No se ingresó la contraseña")]
        public string contra{ get; set; }
        public string correo { get; set; }
        [Required(ErrorMessage = "No se ingresó un correo")]
        public string rol { get; set; }
    }
}
