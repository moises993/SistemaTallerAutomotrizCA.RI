using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entidades
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool CltFrecuente { get; set; }
        public bool FactPendiente { get; set; }
        public string Cedula { get; set; }
    }
}
