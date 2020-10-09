using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrueba.Entidades
{
    public class Expediente
    {
        public int IDExpediente { get; set; }
        public int IDVehiculo { get; set; }
        public string nombreTecnico { get; set; }
        public string asunto { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacionExp { get; set; }
        public string nombreCliente { get; set; }
        public string marca { get; set; }
        public int modelo { get; set; }
        public string placa { get; set; }
    }
}
