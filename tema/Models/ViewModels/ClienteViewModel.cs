using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tema.Models.ViewModels
{
    public class ClienteViewModel
    {
        public Cliente cliente { get; set; }
        public List<DetallesCliente> detallesCliente { get; set; }
    }
}
