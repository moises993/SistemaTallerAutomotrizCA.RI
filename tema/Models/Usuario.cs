using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tema.Models
{
    public class Usuario
    {
        [Display(Name = "Código")]
        public int IDUsuario { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "El correo tiene un formato inválido")]
        [Required(ErrorMessage = "No se ingresó el correo")]
        [Display(Name = "Correo")]
        public string correo { get; set; }
        [Required(ErrorMessage = "No se ingresó una contraseña")]
        public string clave { get; set; }
        [Display(Name = "Rol")]
        public string rol { get; set; }
        public string tokenCorreo { get; set; }
        public string tokenSesion { get; set; }
        public string correoForm { get; set; }
        public string contra { get; set; }
        public string nuevaContrasena { get; set; }
    }
}
