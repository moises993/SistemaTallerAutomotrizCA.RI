using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Entidades
{
    public class Orden
    {
        public int IDOrden { get; set; }
        public int IDVehiculo { get; set; }
        public string cedulaCliente { get; set; }
        public string placaVehiculo { get; set; }
        public string descServicio { get; set; }
    }
}
