using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Entidades
{
    public class Vehiculo
    {
        public int IDVehiculo { get; set; }
        public int IDCliente { get; set; }
        public string marca { get; set; }
        public int modelo { get; set; }
        public string placa { get; set; }

        //no incluido en la bd
        public string cedclt { get; set; }
    }
}
