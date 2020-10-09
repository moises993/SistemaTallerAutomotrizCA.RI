using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Entidades.Vistas
{
    public class ClienteCita
    {
        public string nombre { get; set; }
        public string pmrApellido { get; set; }
        public string sgndApellido { get; set; }
        public string cedula { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string asunto { get; set; }
    }
}
