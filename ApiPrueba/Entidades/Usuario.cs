using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Entidades
{
    public class Usuario
    {
        public int IDUsuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public string rol { get; set; }
        public string tokenCorreo { get; set; }
        public string tokenSesion { get; set; }
    }
}
