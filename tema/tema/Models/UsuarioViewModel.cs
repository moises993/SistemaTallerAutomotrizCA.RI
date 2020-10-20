using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tema.Models
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "No se ingresó un correo")]
        public string correo { get; set; }
        public string rol { get; set; }
    }
}
