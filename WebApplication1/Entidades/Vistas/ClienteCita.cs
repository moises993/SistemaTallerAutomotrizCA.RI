using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entidades.Vistas
{
    public class ClienteCita
    {
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Cedula { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Asunto { get; set; }
    }
}
