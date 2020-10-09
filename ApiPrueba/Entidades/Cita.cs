using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Entidades
{
    public class Cita
    {
        public int IDCita { get; set; }
        public int IDTecnico { get; set; }
        public string cedulaCliente { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string asunto { get; set; }
        public string descripcion { get; set; }
        public bool citaConfirmada { get; set; }
        public int IDCliente { get; set; }
    }
}
