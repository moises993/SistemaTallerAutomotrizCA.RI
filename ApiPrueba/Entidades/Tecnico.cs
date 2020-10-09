using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Entidades
{
    public class Tecnico
    {
        public int IDTecnico { get; set; }
        public string nombre { get; set; }
        public string pmrApellido { get; set; }
        public string sgndApellido { get; set; }
        public string cedula { get; set; }
        public DateTime fechaIngreso { get; set; }
    }
}
