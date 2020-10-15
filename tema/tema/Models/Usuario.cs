using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace tema.Models
{
    public class Usuario
    {
        public int IDUsuario { get; set; }
        [Required(ErrorMessage = "No se ingresó el correo")]
        public string correo { get; set; }
        [Required(ErrorMessage = "No se ingresó una contraseña")]
        public string clave { get; set; }
        public string rol { get; set; }
        public string tokenCorreo { get; set; }
        public string tokenSesion { get; set; }
        public string correoForm { get; set; }
        public string contra { get; set; }
    }
}
