using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tema.Models
{
    public class Vehiculo
    {
        public string cedclt{ get; set; }
        public int IDVehiculo { get; set; }
        public int IDCliente { get; set; }
        public string marca { get; set; }
        public int modelo { get; set; }
        public string placa { get; set; }
    }
}
